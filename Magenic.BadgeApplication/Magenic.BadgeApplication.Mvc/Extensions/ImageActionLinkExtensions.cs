using Magenic.BadgeApplication.Common;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// Interesting implementation for ImageActionLink provided by 
    /// http://stackoverflow.com/questions/341649/asp-net-mvc-ajax-actionlink-with-image
    /// </summary>
    public static class ImageActionLinkExtensions
    {
        private static TagBuilder BuildImageTag(Uri imageUri, string title, object htmlAttributes)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUri.OriginalString);
            builder.MergeAttribute("title", title);
            builder.MergeAttribute("alt", imageUri.GetAltFromImageUri());
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return builder;
        }

        /// <summary>
        /// Builds an ActionLink with the Image as text.
        /// </summary>
        /// <param name="ajaxHelper">The helper.</param>
        /// <param name="imageUri">The image URL.</param>
        /// <param name="title">The title text.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString ImageActionLink(this AjaxHelper ajaxHelper, Uri imageUri, string title, string actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            Arg.IsNotNull(() => imageUri);

            var builder = BuildImageTag(imageUri, title, htmlAttributes);
            var link = ajaxHelper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions).ToHtmlString();
            return MvcHtmlString.Create(link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
        }

        /// <summary>
        /// Builds an ActionLink with the Image as text.
        /// </summary>
        /// <param name="ajaxHelper">The ajax helper.</param>
        /// <param name="imageUri">The image URL.</param>
        /// <param name="title">The title text.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="ajaxOptions">The ajax options.</param>
        /// <returns></returns>
        public static IHtmlString ImageActionLink(this AjaxHelper ajaxHelper, Uri imageUri, string title, string actionName, object routeValues, AjaxOptions ajaxOptions)
        {
            Arg.IsNotNull(() => imageUri);
            return ImageActionLink(ajaxHelper, imageUri, title, actionName, routeValues, ajaxOptions, null);
        }

        /// <summary>
        /// Builds an ActionLink with the Image as text.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="imageUri">The image URL.</param>
        /// <param name="title">The title text.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, Uri imageUri, string title, string actionName, object routeValues, object htmlAttributes)
        {
            Arg.IsNotNull(() => imageUri);

            var builder = BuildImageTag(imageUri, title, htmlAttributes);
            var link = htmlHelper.ActionLink("[replaceme]", actionName, routeValues).ToHtmlString();
            return MvcHtmlString.Create(link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
        }

        /// <summary>
        /// Builds an ActionLink with the Image as text.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="imageUri">The image URL.</param>
        /// <param name="title">The title text.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns></returns>
        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, Uri imageUri, string title, string actionName, object routeValues)
        {
            Arg.IsNotNull(() => imageUri);
            return ImageActionLink(htmlHelper, imageUri, title, actionName, routeValues);
        }

        /// <summary>
        /// Builds an ActionLink with the Image as text.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="imageUri">The image URI.</param>
        /// <param name="title">The title.</param>
        /// <param name="actionResult">The action result.</param>
        /// <returns></returns>
        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, Uri imageUri, string title, ActionResult actionResult)
        {
            Arg.IsNotNull(() => htmlHelper);
            return ImageActionLink(htmlHelper, imageUri, title, actionResult, null);
        }

        /// <summary>
        /// Builds an ActionLink with the Image as text.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="imageUri">The image URI.</param>
        /// <param name="title">The title.</param>
        /// <param name="actionResult">The action result.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <returns></returns>
        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, Uri imageUri, string title, ActionResult actionResult, object htmlAttributes)
        {
            Arg.IsNotNull(() => htmlHelper);
            var builder = BuildImageTag(imageUri, title, htmlAttributes);
            var link = htmlHelper.ActionLink("[replaceme]", actionResult).ToHtmlString();
            return MvcHtmlString.Create(link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
        }
    }
}