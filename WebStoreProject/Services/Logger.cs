using System;
using System.IO;

namespace WebStoreProject.Services
{
    public class Logger : ILogger
    {
        public void WriteLog(string strLog, Catgory catgory)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;
            string logFilePath;
            if (catgory == Catgory.User)
            {
                logFilePath = @"C:\Users\yahav\source\repos\WebStoreProject\Logger\Product\";
            }
            else {logFilePath = @"C:\Users\yahav\source\repos\WebStoreProject\Logger\User\"; }
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(strLog + " - " + DateTime.Now.ToShortTimeString());
            log.Close();
        }
    }

    public enum Catgory
    {
        Product = 0,
        User
    }
}
