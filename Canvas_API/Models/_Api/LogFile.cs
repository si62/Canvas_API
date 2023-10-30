using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Canvas_API.Models._Api
{
    public class LogFile
    {
        string file_name;
        public LogFile() {
            file_name = Directory.GetCurrentDirectory() + @"\Logs\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_log.txt";
            if (!File.Exists(file_name))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(file_name))
                {
                    sw.WriteLine("Log\r\n");
                }
            }
        }
        public void Log(string logMessage)
        {
            using (StreamWriter w = File.AppendText(file_name))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.WriteLine("  :");
                w.WriteLine($"  :{logMessage}");
                w.WriteLine("-------------------------------");
            }

        }
    }
}
