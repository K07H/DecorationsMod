using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DecorationsModConfigurator
{
    public static class Logger
    {
        public static string LogFilePath = null;
        public static void Log(string text, params object[] args)
        {
            if (args != null && args.Length > 0)
                text = string.Format(text, args);


            if (LogFilePath == null)
            {
                string configuratorFolder = Path.GetDirectoryName(Uri.UnescapeDataString(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath));
                LogFilePath = Uri.UnescapeDataString(new Uri(Path.Combine(configuratorFolder, "Logs.txt")).AbsolutePath);
                if (File.Exists(LogFilePath))
                {
                    try { File.Delete(LogFilePath); }
                    catch { }
                }
            }
            if (LogFilePath != null)
            {
                try { File.AppendAllText(LogFilePath, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff") + " : " + text + Environment.NewLine); }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: Could not log message [{0}]. Exception=[{1}]", text, ex.ToString());
                }
            }
        }
    }
}
