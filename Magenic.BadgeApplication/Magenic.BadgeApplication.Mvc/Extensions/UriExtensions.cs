using Magenic.BadgeApplication.Common;
using System;
using System.Linq;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Gets the title from image URI.
        /// </summary>
        /// <param name="imageUri">The image URI.</param>
        /// <returns></returns>
        public static string GetAltFromImageUri(this Uri imageUri)
        {
            Arg.IsNotNull(() => imageUri);

            return imageUri.OriginalString.Split('/').Last().Replace('_', '.');
        }
    }
}