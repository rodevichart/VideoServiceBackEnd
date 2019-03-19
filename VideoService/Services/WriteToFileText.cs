using System;
using System.IO;
using VideoService.Services.Interfaces;

namespace VideoService.Services
{
    public class WriteToFileText : IWriteToFileText
    {
        private object _lock = new object();

        public void WriteToFile(string path, string writeText)
        {
            try
            {
                lock (_lock)
                {
                    using (var sw = !File.Exists(path) ? File.CreateText(path) : File.AppendText(path))
                    {
                        sw.WriteLine(writeText);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}