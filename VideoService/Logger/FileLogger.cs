using System;
using Microsoft.Extensions.Logging;
using VideoService.Services.Interfaces;

namespace VideoService.Logger
{
    public class FileLogger : ILogger
    {
        private readonly IWriteToFileText _writeToFileText;
        private readonly string _filePath;

        public FileLogger(IWriteToFileText writeToFileText, string path)
        {
            _writeToFileText = writeToFileText;
            _filePath = path;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                var inputText = formatter(state, exception) + Environment.NewLine;
                _writeToFileText.WriteToFile(_filePath, inputText);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}