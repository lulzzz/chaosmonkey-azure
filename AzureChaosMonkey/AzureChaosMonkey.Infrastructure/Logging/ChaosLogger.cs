using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureChaosMonkey.Infrastructure.TimeProvider;

namespace AzureChaosMonkey.Infrastructure.Logging
{
    public class ChaosLogger : IChaosLogger, IDisposable
    {
        private readonly ITimeProvider _timeProvider;
        private readonly StreamWriter _logStream;

        public ChaosLogger(string logFileName,
                           ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _logStream = !string.IsNullOrEmpty(logFileName) ? File.AppendText(logFileName) : new StreamWriter(Console.OpenStandardOutput());
            _logStream.AutoFlush = true;
        }

        public void Log(string message)
        {
            var logText = string.Concat($"{_timeProvider.UtcNow} UTC", " - ", message);
            _logStream?.WriteLine(logText);
        }

        public void Dispose()
        {
            _logStream?.Close();
        }
    }
}
