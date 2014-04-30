using Magenic.BadgeApplication.Common.Enums;
using System;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ImageController
        : AsyncController
    {
        /// <summary>
        /// Profiles the photo.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public virtual async Task<ActionResult> ProfilePhoto(string size, string userName)
        {
            var photoSize = (PhotoSize)Enum.Parse(typeof(PhotoSize), size);
            var profileUrlFormat = ConfigurationManager.AppSettings["ProfilePhoto"];
            var profileUrlString = String.Format(CultureInfo.CurrentCulture, profileUrlFormat, userName);

            byte[] emptyPhoto = await FetchEmptyPhoto(photoSize);
            byte[] blobBytes = await DownloadProfilePhoto(profileUrlString, emptyPhoto);

            var webImage = ResizePhoto(photoSize, blobBytes);
            var mimeType = HandleMimeType(webImage);

            return File(webImage.GetBytes(), mimeType);
        }

        private static string HandleMimeType(WebImage webImage)
        {
            var mimeType = String.Empty;
            switch (webImage.ImageFormat)
            {
                case "jpeg":
                case "jpg":
                    mimeType = "image/jpeg";
                    break;

                default:
                    mimeType = "image/jpeg";
                    break;
            }

            return mimeType;
        }

        private static WebImage ResizePhoto(PhotoSize photoSize, byte[] blobBytes)
        {
            var webImage = new WebImage(blobBytes);
            switch (photoSize)
            {
                case PhotoSize.Small:
                    webImage = webImage.Resize(64, 64, true, true);
                    break;

                case PhotoSize.Medium:
                    webImage = webImage.Resize(128, 128, true, true);
                    break;

                case PhotoSize.Large:
                    webImage = webImage.Resize(256, 256, true, true);
                    break;
            }

            return webImage;
        }

        private static async Task<byte[]> DownloadProfilePhoto(string profileUrlString, byte[] emptyPhotoBytes)
        {
            var webClient = new WebClient();
            webClient.UseDefaultCredentials = true;

            byte[] blobBytes = new byte[emptyPhotoBytes.Length];
            try
            {
                blobBytes = await webClient.DownloadDataTaskAsync(profileUrlString);
            }
            catch (WebException webException)
            {
                if (webException.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)webException.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        emptyPhotoBytes.CopyTo(blobBytes, 0);
                    }
                }
                else if (webException.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    emptyPhotoBytes.CopyTo(blobBytes, 0);
                }
                else
                {
                    throw webException;
                }
            }

            return blobBytes;
        }

        private async Task<byte[]> FetchEmptyPhoto(PhotoSize photoSize)
        {
            var webClient = new WebClient();
            webClient.UseDefaultCredentials = true;

            var uri = ControllerContext.HttpContext.Request.Url;
            var baseUri = new Uri(uri.GetLeftPart(UriPartial.Authority));
            byte[] emptyPhotoBytes = null;
            switch (photoSize)
            {
                case PhotoSize.Small:
                case PhotoSize.Medium:
                    var mediumUri = new Uri(baseUri, Links.Content.Images.emptyPhotoM_png);
                    emptyPhotoBytes = await webClient.DownloadDataTaskAsync(mediumUri);
                    break;

                case PhotoSize.Large:
                    var largeUri = new Uri(baseUri, Links.Content.Images.emptyPhotoL_png);
                    emptyPhotoBytes = await webClient.DownloadDataTaskAsync(largeUri);
                    break;
            }

            return emptyPhotoBytes;
        }
    }
}