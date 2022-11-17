using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulator.Util
{
    public class LogHelper
    {
        private List<string> _logMessageList = new List<string>();

        public void AddLogMessage(string subject, string message)
        {
            _logMessageList.Add(message);

            string currentDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ImageManipulator");

            DirectoryInfo di = new DirectoryInfo(currentDir);
            if (!di.Exists)
                di.Create();

            string logDir = Path.Combine(currentDir, "Log");

            DirectoryInfo logDi = new DirectoryInfo(logDir);
            if (!logDi.Exists)
                logDi.Create();

            string date = DateTime.Now.ToString("yyyyMMdd") + ".txt";

            string filePath = Path.Combine(logDir, date);

            FileInfo logInfo = new FileInfo(filePath);
            if (!logInfo.Exists)
                logInfo.Create().Close();

            StreamWriter sr = new StreamWriter(filePath, true);

            if(message == "")
                sr.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "]" + subject);
            else
                sr.WriteLine( "[" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "]" + subject + " : "+ message);

            sr.Close();
        }
    }
}
