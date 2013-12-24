using System;
using System.Linq;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets the file name from path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string GetFileNameFromPath(this string path)
        {
            if (!String.IsNullOrWhiteSpace(path))
            {
                var uri = new Uri(path);
                return uri.AbsolutePath.Split('/').Last();
            }

            return String.Empty;
        }
    }
}