using System.IO;
using Microsoft.Extensions.Logging;
using VideoService.Extensions;
using VideoService.Services.Interfaces;

namespace VideoService.Configurations
{
    public static class LoggerConfigurations
    {
        public static ILogger Configure(ILoggerFactory loggerFactory, IWriteToFileText writeToFileText)
        {
            loggerFactory.AddFile(writeToFileText,
                Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");
            return logger;
        }
    }
}