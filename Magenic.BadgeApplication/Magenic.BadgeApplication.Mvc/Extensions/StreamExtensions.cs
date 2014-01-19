using Magenic.BadgeApplication.Common;
using System.IO;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static byte[] GetBytes(this Stream stream)
        {
            Arg.IsNotNull(() => stream);

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}