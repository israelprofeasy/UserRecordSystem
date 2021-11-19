using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UserRecordKeepingSystem.Commons
{
    
        public class Logger :  ILogger
        {
        readonly string path = Utilities.GetApsolutePath("/Logs.txt");
        public void Log(string items)
            {
            if (File.Exists(path))
            {
                File.AppendAllText(path, $"{DateTime.Now.Date}{DateTime.Now.TimeOfDay} " + items + "\n");
            }
            else File.WriteAllText(path, $"{DateTime.Now.Date}{DateTime.Now.TimeOfDay} " + items + "\n");
        }
        }
    
}
