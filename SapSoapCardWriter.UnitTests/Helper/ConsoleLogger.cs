using SapSoapCardWriter.Logger.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SapSoapCardWriter.UnitTests.Helper
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void Info(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void Info(LogEventBase logEvent)
        {
            throw new NotImplementedException();
        }

        public void Warning(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void Warning(Exception error, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void Error(Exception error, string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void Fatal(Exception error, string format, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
