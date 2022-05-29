using System;
using System.Collections.Generic;
using System.Text;

namespace AjGrantRefactorChallenge
{
    public class Logger
    {
        private Logger() { }

        private static readonly Logger _instance = new Logger();

        public static Logger Instance
        {
            get { return _instance; }
        }

        public void LogInformation(string logEntry)
        {
            Console.WriteLine(logEntry);
        }
    }
}
