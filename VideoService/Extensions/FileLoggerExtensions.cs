using Microsoft.Extensions.Logging;
using VideoService.Logger;
using VideoService.Services.Interfaces;

namespace VideoService.Extensions
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, IWriteToFileText writeToFileText,
            string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(writeToFileText,filePath));
            return factory;
        }
    }
}