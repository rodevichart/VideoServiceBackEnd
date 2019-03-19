using Microsoft.Extensions.Logging;
using VideoService.Services.Interfaces;

namespace VideoService.Logger
{
    public class FileLoggerProvider: ILoggerProvider
    {
        private readonly string _path;
        private readonly IWriteToFileText _writeToFileText;

        public FileLoggerProvider(IWriteToFileText writeToFileText, string path)
        {
            _writeToFileText = writeToFileText;
            _path = path;
        }
        
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_writeToFileText, _path);
        }

    }
}