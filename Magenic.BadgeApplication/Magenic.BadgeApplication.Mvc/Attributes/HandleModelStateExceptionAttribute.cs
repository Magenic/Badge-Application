using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Exceptions;
using System;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace Magenic.BadgeApplication.Attributes
{
    /// <summary>
    /// <remarks>
    /// Based on content from this site http://erraticdev.blogspot.com/2010/11/handling-validation-errors-on-ajax.html
    /// </remarks>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class HandleModelStateExceptionAttribute
        : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Called when an exception occurs and processes <see cref="ModelStateException"/> object.
        /// </summary>
        /// <param name="filterContext">Filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            Arg.IsNotNull(() => filterContext);

            // handle modelStateException
            if (filterContext.Exception != null && typeof(ModelStateException).IsInstanceOfType(filterContext.Exception) && !filterContext.ExceptionHandled)
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.ContentEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.HeaderEncoding = Encoding.UTF8;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                filterContext.Result = new ContentResult
                {
                    Content = (filterContext.Exception as ModelStateException).Message,
                    ContentEncoding = Encoding.UTF8,
                };
            }
        }
    }
}