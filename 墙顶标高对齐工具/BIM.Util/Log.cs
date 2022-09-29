using System;
using System.IO;

namespace BIM.Util
{
    public class Log
    {
        /// <summary>
        /// log filepath
        /// </summary>
        private static string LogFullpath => Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "LogBIM.log");

        /// <summary>
        /// 日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="category"></param>
        public static void Debug<T>(T message, string category = "debug")
        {
            using (var fs = new FileStream(LogFullpath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine($"{DateTime.Now:s}|{category}|{message}|");
                    sw.Flush();
                }
            }
        }
    }
}
