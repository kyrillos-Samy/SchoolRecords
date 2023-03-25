using Common.Logger.Contract;
using System.Text;
using SystemPath = System.IO.Path;

namespace Common.Logger.Implementation
{
    public class FileLogger : ILoggerBase
    {
        private readonly ILoggerBase _logger = null;
        private readonly string _loggingPath;
        private readonly string _fileNameFormat = "{0}-Logs-{1}.txt";
        private static object obj = new object();

        public FileLogger()
        {
            _loggingPath = SystemPath.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(_loggingPath))
            {
                Directory.CreateDirectory(_loggingPath);

            }
        }
        public void LogInfo(string msg)
        {
            try
            {
                _logger?.LogInfo(msg);
                string filePath = GetFilePath();
                Log("info", msg, filePath);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Log(string messageType, string logMessage, string filePath)
        {
            StringBuilder message = new StringBuilder();
            try
            {
                message.AppendLine(string.Format("\r\n[{0}] - Log Entry : ", messageType.ToUpper()));
                message.AppendLine(string.Format("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString()));
                message.AppendLine(string.Format(new string('-', 60)));
                message.AppendLine(string.Format("  :{0}", logMessage));
                message.AppendLine(string.Format(new string('-', 60)));

                lock (obj)
                {
                    System.IO.File.AppendAllText(filePath, message.ToString());
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }


        private string GetFilePath()
        {
            return SystemPath.Combine(_loggingPath, string.Format(_fileNameFormat, "INFO",
                DateTime.Today.ToString("dd-MM-yyyy") + "-h-" + DateTime.Now.Hour));
        }
    }
}
