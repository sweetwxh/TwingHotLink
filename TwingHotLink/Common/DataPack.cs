using TwingHotLink.Tools;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * Copryright:StepOn Technology
 * */

namespace TwingHotLink.Common
{
    /// <summary>
    ///     网络数据封包工具
    /// </summary>
    internal class DataPack
    {
        /// <summary>
        ///     打包要发送的数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dataToPack"></param>
        /// <returns></returns>
        public static byte[] GetPackedData(byte code, byte[] dataToPack)
        {
            var totalLength = 9 + dataToPack.Length;
            var lengthBin = NumberTranslate.Int32ToBinary(totalLength);
            var headBin = new byte[9];
            NetData.Head.CopyTo(headBin, 0);
            lengthBin.CopyTo(headBin, 4);
            headBin[8] = code;

            var sendBin = new byte[totalLength];
            headBin.CopyTo(sendBin, 0);
            dataToPack.CopyTo(sendBin, 9);

            return sendBin;
        }
    }
}