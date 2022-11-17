using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XManager.Utill
{
    public class ErrorLog
    {
        private string _logPath;
        private StreamWriter _streamWriter;
        public void ErrorLogInitializate()
        {
            //string dbPath = CStatus.Instance().LocalDBPath;
            string dbPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string folderPath = Path.Combine(dbPath, "Log");

            DirectoryInfo ErrorFolderPath = new DirectoryInfo(folderPath);

            if (ErrorFolderPath.Exists == false)
                ErrorFolderPath.Create();

            string TodayFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            string filePath = Path.Combine(folderPath, TodayFileName);
            FileInfo errorPath = new FileInfo(filePath);

            if(errorPath.Exists == false)
            {
                errorPath.Create().Close();
            }
            this._logPath = filePath;
         
        }
        
        public void WriteErrorLog(string fuctionName, string message)
        {
            this._streamWriter = new StreamWriter(this._logPath, true);
            string nowTime = "["+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";
            string errorMessage = nowTime + " FuctionName : " + fuctionName + "     Message : " + message;
    
            this._streamWriter.WriteLine(errorMessage);
            this._streamWriter.Close();
        }

        public void Close()
        {
            this._streamWriter.Close();
        }
    }
}
