using System;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * Copryright:StepOn Technology
 * */

namespace TwingHotLink.Tools
{
    /// <summary>
    ///     错误日志记录
    /// </summary>
    internal class ErrorLogger
    {
        /// <summary>
        ///     记录异常日志信息
        /// </summary>
        /// <param name="ex">异常</param>
        public static void LogException(Exception ex)
        {
            //关闭异常记录
            /*
            string errDirectory = Environment.CurrentDirectory + "/errorLog/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
            string errFileName = string.Format("{0:yyyy-MM-dd}.txt", DateTime.Now);
            string savePath = errDirectory + errFileName;
            if (!Directory.Exists(errDirectory))
            {
                Directory.CreateDirectory(errDirectory);
            }
            File.AppendAllText(savePath, String.Format("{0:yyyy-MM-dd HH:mm:ss}::{1}{2}{2}", DateTime.Now, ex, Environment.NewLine));
            */
        }
    }
}