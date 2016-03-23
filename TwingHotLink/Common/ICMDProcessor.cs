using System.Net;
using TwingHotLink.Server;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * Copryright:StepOn Technology
 * */

namespace TwingHotLink.Common
{
    internal interface ICMDProcessor
    {
        void ProcessCMD(byte code, byte[] data, ClientInfo client, EndPoint clientEndPoint);
    }
}