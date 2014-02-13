using System.Text;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Generates the conditional navigation.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="linkText">The link text.</param>
        /// <param name="actionResult">The action result.</param>
        /// <returns></returns>
        public static MvcHtmlString GenerateConditionalNavigation(this HtmlHelper html, string linkText, ActionResult actionResult)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<li>");
            stringBuilder.Append(html.ActionLink(linkText, actionResult).ToHtmlString());
            stringBuilder.Append("</li>");
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}