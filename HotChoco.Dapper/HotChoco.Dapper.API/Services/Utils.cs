﻿namespace HotChoco.Dapper.API.Services
{
    public class Utils
    {
        private readonly IConfiguration _configuration;

        public Utils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static void WriteLog(string strLog, string issuefolder)

        {
            int xt = 0;
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = "C:\\adminisLogs\\" + issuefolder + "\\";
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
            log.WriteLine(strLog + " at " + System.DateTime.Now.ToString());
            log.Close();

        }
    }
}
