using System;

namespace IfiNavetNotifier.Logger
{
    public class ConsoleLogger : ILogger
    {
        public LoggLevel CurrentLoggLevel { get; set; }
        public ConsoleLogger(LoggLevel loggLevel = LoggLevel.Info)
        {
            //TODO: Create possibility to set loglevel in the Environmental variable
            CurrentLoggLevel = loggLevel;
        }    
        [Flags]
        public enum LoggLevel
        {
            None,
            Info = 1, 
            Warning = 2, 
            Debugg = Info | Warning
        }

        public void Informtion(string info)
        {
            if(CurrentLoggLevel == LoggLevel.Info)
                logToConsole(info);
        }

        public void Warning(string warning)
        {
            if(CurrentLoggLevel == LoggLevel.Warning)
                logToConsole(warning);
        }

        public void Debug(string info)
        {
            if(CurrentLoggLevel == LoggLevel.Debugg)
                logToConsole(info);        
        }

        public void Exception(string info, Exception ex)
        {
            logToConsole(info + " - "+ ex.Message);
            Console.WriteLine("test");
        }

        private void logToConsole(string message)
        {
            Console.WriteLine(DateTime.Now + ": "+message);
        }
    }
}