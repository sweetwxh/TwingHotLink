using System.Collections.Generic;
using System.Net.Sockets;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * Copryright:StepOn Technology
 * */

namespace TwingHotLink.Server.Extensions
{
    internal sealed class BufferPool
    {
        /// <summary>
        ///     缓冲池管理的缓冲数据
        /// </summary>
        private readonly byte[] buffer;

        /// <summary>
        ///     缓冲数据长度
        /// </summary>
        private readonly int bufferSize;

        /// <summary>
        ///     当前的序列
        /// </summary>
        private int currentIndex;

        /// <summary>
        ///     数据池序列
        /// </summary>
        private readonly Stack<int> freeIndexPool;

        /// <summary>
        ///     该数据池的总字节大小
        /// </summary>
        private readonly int totalBytes;

        /// <summary>
        ///     数据缓冲池
        /// </summary>
        /// <param name="totalBytes"></param>
        /// <param name="bufferSize"></param>
        internal BufferPool(int totalBytes, int bufferSize)
        {
            this.totalBytes = totalBytes;
            currentIndex = 0;
            this.bufferSize = bufferSize;
            freeIndexPool = new Stack<int>();
            buffer = new byte[totalBytes];
        }

        /// <summary>
        ///     获取还可以进行分配的缓冲数据大小
        /// </summary>
        internal int Available
        {
            get
            {
                lock (freeIndexPool)
                {
                    return (totalBytes - currentIndex)/bufferSize + freeIndexPool.Count;
                }
            }
        }

        /// <summary>
        ///     设置缓冲数据
        /// </summary>
        /// <param name="args">需要缓冲数据的SocketAsyncEventArgs</param>
        internal void SetBuffer(SocketAsyncEventArgs args)
        {
            lock (freeIndexPool)
            {
                if (freeIndexPool.Count > 0)
                {
                    args.SetBuffer(buffer, freeIndexPool.Pop(), bufferSize);
                }
                else
                {
                    args.SetBuffer(buffer, currentIndex, bufferSize);
                    currentIndex += bufferSize;
                }
            }
        }

        /// <summary>
        ///     释放缓冲数据
        /// </summary>
        /// <param name="args">需要释放缓冲数据的SocketAsyncEventArgs</param>
        internal void FreeBuffer(SocketAsyncEventArgs args)
        {
            lock (freeIndexPool)
            {
                freeIndexPool.Push(args.Offset);
                args.SetBuffer(null, 0, 0);
            }
        }
    }
}