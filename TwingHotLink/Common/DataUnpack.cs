using System;
using System.Net;
using System.Text;
using TwingHotLink.Server;
using TwingHotLink.Tools;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * Copryright:StepOn Technology
 * */

namespace TwingHotLink.Common
{
    /// <summary>
    ///     网络数据拆包工具
    /// </summary>
    internal class DataUnpack
    {
        private readonly ClientInfo client;

        private readonly EndPoint clientEndPoint;
        private int processCount;

        private readonly ICMDProcessor processor;

        public DataUnpack(ICMDProcessor processor, ClientInfo client, EndPoint clientEndPoint)
        {
            this.client = client;
            this.processor = processor;
            this.clientEndPoint = clientEndPoint;
        }

        /// <summary>
        ///     开始处理接收到的数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="offset"></param>
        /// <param name="revLength"></param>
        /// <param name="lastDataBuf"></param>
        public void ProcessBinaryData(byte[] source, int offset, int revLength, ref byte[] lastDataBuf)
        {
            /************************************************************************************
            * 说明：
            * 处理二进制数据一共会出现一下几种情况需要处理。
            * 第一种：
            * 长度信息（PS:包含包头和长度本身的8个字节）=接受到的数据长度（开头包含包头）
            * 则表示为一个完整数据包，直接进行处理。如果客户端信息的最后缓存当中还存在数据，则
            * 已经不需要了（因为此次受到的数据是完整数据了）
            * 
            * 第二种：
            * 长度信息<接受到的信息（开头包含包头）
            * 则除了一个完整数据本身外，还包含第二条指令数据（粘包），这种情况就以此取出
            * 完整数据进行处理，直到第三种情况出现或者数据全部处理完成。
            * 
            * 第三种：
            * 长度信息>接受到的信息(开头包含包头)
            * 不进行处理，直接放到缓存当中等待下一次数据。如果下一次数据是1，2种情况，则丢弃缓
            * 存数据。
            * 
            * 第四种：
            * 不包含包头信息
            * 这个情况属于前一次发包出现第三种情况（或向前的几次，此后出现这种情况），则将此部分
            * 数据附加到最后缓存中进行处理。
            * 
            * 第五种：
            * 接收到的数据长度小于8
            * 相似于第四种，也是附加到最后缓存中调用自己本身进行那个处理。
            * **********************************************************************************/

            if (GlobleSetting.ProcessLimit != 0)
            {
                if (processCount > GlobleSetting.ProcessLimit)
                {
                    return;
                }
            }
            var packetStart = true;
            //处理第五种情况
            processCount++;
            if (revLength < 8)
            {
                if (lastDataBuf == null)
                {
                    //如果没有最后缓存，则抛弃此数据
                    return;
                }
                //附加到最后缓存中
                var newbuf = new byte[lastDataBuf.Length + revLength];
                lastDataBuf.CopyTo(newbuf, 0);
                Array.Copy(source, offset, newbuf, lastDataBuf.Length, revLength);
                ProcessBinaryData(newbuf, 0, newbuf.Length, ref lastDataBuf);
                return;
            }
            var currentpostion = offset; //数组下标指示
            var revData = source; //接受到的数据
            //检查包头信息
            for (var i = offset; i < offset + 4; i++)
            {
                if (revData[i] != NetData.Head[i - offset])
                {
                    packetStart = false;
                }
            }
            currentpostion = offset + 4;

            if (packetStart)
            {
                //包含包头信息的情况（1，2，3）
                var ltBuf = new byte[4];
                Array.Copy(revData, currentpostion, ltBuf, 0, 4);
                //将长度二进制状换为int
                var allLength = NumberTranslate.BinaryToInt32(ltBuf);
                if (allLength > 65535)
                {
                    //异常数据，抛去并清空最后缓存
                    lastDataBuf = null;
                    return;
                }
                //检查接受到的实际数据长度是否等于包中的长度
                if (allLength == revLength)
                {
                    //一般情况（1）
                    ProcessSinglePacket(revData, offset, allLength);
                    //当数据是完整的数据包时，清空最后非完整数据数据缓存
                    lastDataBuf = null;
                }
                else if (allLength < revLength)
                {
                    //第二种情况
                    ProcessSinglePacket(revData, offset, allLength); //将完整的数据包加入到队列
                    lastDataBuf = null;
                    var lastLength = revLength - allLength; //获取剩下的数据长度
                    ProcessBinaryData(revData, offset + allLength, lastLength, ref lastDataBuf);
                }
                else
                {
                    //第三种情况
                    lastDataBuf = new byte[revLength];
                    Array.Copy(revData, offset, lastDataBuf, 0, revLength);
                }
            }
            else
            {
                //这是第四种情况，不包含包头信息的
                byte[] newbuf = null;
                if (lastDataBuf == null)
                {
                //非新数据但无非完整缓冲，则查找此数据包是否存在包头信息，如果没有，则丢掉该数据包
                    newbuf = new byte[revLength];
                    Array.Copy(revData, offset, newbuf, 0, revLength);
                }
                else
                {
                    var lastbuf = lastDataBuf; //得到上次剩余数据
                    var totalLength = lastbuf.Length + revLength;
                    newbuf = new byte[totalLength];
                    //将上次剩余包放入新缓存中
                    lastbuf.CopyTo(newbuf, 0);
                    //将本次数据包拷贝到新缓存中
                    Array.Copy(revData, offset, newbuf, lastbuf.Length, revLength);
                    /*
                     * 说明：正常情况下，如果客户端连续发包，则可能造成两个完整包或者一个完整包一次发送到服务器的情况。
                     * 此时如果采用此函数处理数据包，则可能出现的情况为：有一个以上完整包，则此函数将提取一个完整包后
                     * 采用回调的方式将剩余的数据和长度做为一个新的接受数据传递给本身，在次进行数据的解析工作。过程
                     * 同上，但是如果数据包的完整长度大于了剩下的长度，则可能是数据包没有发送完毕（分两次以上发送，接受
                     * 数据的缓存是1024bytes），此时，函数将非完整数据放入一个待处理缓存中。按照正常情况（网络连接良好，
                     * 没有掉包显现），则在这个剩余缓存中存储的数据开头4位一定是包头信息。因为处理的数据应该是完整的，
                     * 连续的，处理完毕一个之后，下一个没有处理完毕的将把开头拷贝到缓存中（如果缓存中有数据但是下一次接到
                     * 新的数据却包含包头信息时，将清空此缓存）。但在网络不稳定的情况下，可能出现掉包现象，则此时可能不能
                     * 得到正确的数据，所以此处检查在新数据是没有包含包头信息的数据时，则与上次的剩余数据合并，合并之后
                     * 检查是否开头含有包头信息，如果没有，则表示上次的信息出错，则查找本次数据是否含有包头数据，有，则从
                     * 开始出提取数据并在此Call本函数进行处理，并清空剩余缓存（此时剩余缓存中的数据为错误数据了）
                     */
                }
                //检查是否存在包头
                var tempStart = true;
                for (var i = 0; i < 4; i++)
                {
                    if (newbuf[i] != NetData.Head[i])
                    {
                        tempStart = false;
                    }
                }
                if (tempStart)
                {
                    ProcessBinaryData(newbuf, 0, newbuf.Length, ref lastDataBuf);
                }
                else
                {
//查找下一个包头信息
                    var slookup = Encoding.ASCII.GetString(NetData.Head); //将二进制数据转换为string方便查询
                    var ssource = Encoding.ASCII.GetString(newbuf);
                    //查找包头
                    var headIndex = ssource.IndexOf(slookup);
                    if (headIndex != -1)
                    {
//存在包头
                        var lastLength = newbuf.Length - headIndex;
                        lastDataBuf = null;
                        ProcessBinaryData(newbuf, headIndex, lastLength, ref lastDataBuf); //处理数据
                    }
                    else
                    {
//不存在包头
                        lastDataBuf = null;
                    }
                }
            }
        }

        private void ProcessSinglePacket(byte[] fullData, int Offset, int length)
        {
            var dataLength = length - 8;
            var logicData = new byte[dataLength];
            Array.Copy(fullData, Offset + 8, logicData, 0, dataLength);

            //进行逻辑数据的分析
            var code = logicData[0]; //取得命令
            var realData = new byte[dataLength - 1];
            Array.Copy(logicData, 1, realData, 0, dataLength - 1);
            processor.ProcessCMD(code, realData, client, clientEndPoint);
        }
    }
}