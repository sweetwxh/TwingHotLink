/***
* 作者：吴兴华
* 日期：2010-12-16
* Copryright:StepOn Technology
* */

namespace TwingHotLink.Tools
{
    internal class NumberTranslate
    {
        /// <summary>
        ///     将二进制转换为十进制
        /// </summary>
        /// <param name="source">需要转换的二进制数据</param>
        /// <returns>转换后的十进制数</returns>
        public static int BinaryToInt32(byte[] source)
        {
            var result = 0;
            result = (source[0] << 24) | (source[1] << 16) | (source[2] << 8) | source[3];
            return result;
        }

        /// <summary>
        ///     将十进制转换为二进制数据
        /// </summary>
        /// <param name="source">需要转换的十进制数</param>
        /// <returns>转换后的二进制数</returns>
        public static byte[] Int32ToBinary(int source)
        {
            var result = new byte[4];
            result[0] = (byte) ((source >> 24) & 0xff);
            result[1] = (byte) ((source >> 16) & 0xff);
            result[2] = (byte) ((source >> 8) & 0xff);
            result[3] = (byte) (source & 0xff);
            return result;
        }
    }
}