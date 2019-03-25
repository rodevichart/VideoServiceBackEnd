using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;

namespace VideoServiceBL
{
    public class PhotoSettings
    {
        public int MaxBytes { get; set; }

        public List<string> AcceptedFileTypes { get; set; }

        public bool IsSupported(Image originalImage)
        {
            var supportedTypes = new List<ImageFormat>();
            var type = typeof(ImageFormat);

                foreach (var acceptedFileType in AcceptedFileTypes)
                {
                    var value = type
                        .GetProperty(acceptedFileType, BindingFlags.Static | BindingFlags.Public)
                        ?.GetValue(null, null);
                    if (value != null)
                    {
                        supportedTypes.Add(value as ImageFormat);
                    }
                }
                return supportedTypes.All(s => !Equals(s, originalImage.RawFormat));
        }
    }
}