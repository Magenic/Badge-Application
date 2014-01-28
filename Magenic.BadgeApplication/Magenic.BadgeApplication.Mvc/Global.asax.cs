using Csla;
using Magenic.BadgeApplication.BusinessLogic.Security;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Magenic.BadgeApplication
{
    /// <summary>
    /// 
    /// </summary>
    public class MvcApplication
        : HttpApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MvcApplication"/> class.
        /// </summary>
        public MvcApplication()
        {
            this.AddOnAuthenticateRequestAsync(BeginApplication_AuthenticateRequest, EndApplication_AuthenticateRequest);
        }

        /// <summary>
        /// The application entry point
        /// </summary>
        protected void Application_Start()
        {
            ModelBinders.Binders.DefaultBinder = new Csla.Web.Mvc.CslaModelBinder();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacContainer.RegisterDependencies();
        }

        /// <summary>
        /// An async way of running Authenticate_Request. That isn't supported with await or async yet.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <param name="asyncCallback">The asynchronous callback.</param>
        /// <param name="objectState">State of the object.</param>
        /// <returns></returns>
        protected IAsyncResult BeginApplication_AuthenticateRequest(Object source, EventArgs eventArgs, AsyncCallback asyncCallback, Object objectState)
        {
            var task = this.Application_AuthenticateRequest(source, eventArgs);
            var taskCompletionSource = new TaskCompletionSource<bool>(objectState);

            task.ContinueWith(t =>
            {
                if (task.IsFaulted && task.Exception != null)
                {
                    taskCompletionSource.TrySetException(task.Exception.InnerExceptions);
                }
                else if (task.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(true);
                }

                if (asyncCallback != null)
                {
                    asyncCallback(taskCompletionSource.Task);
                }
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);

            return taskCompletionSource.Task;
        }

        /// <summary>
        /// This happens after the BeginApplication_AuthenticateRequest completes.
        /// </summary>
        /// <param name="asyncResult">The asynchronous result.</param>
        protected void EndApplication_AuthenticateRequest(IAsyncResult asyncResult)
        {
            // Nothing to do here...
        }

        /// <summary>
        /// Correctly sets the CSLA principal/Identity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected async Task Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var formsCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (formsCookie != null)
            {
                var authenticationToken = FormsAuthentication.Decrypt(formsCookie.Value);
                if (authenticationToken != null)
                {
                    var customPrincipal = await CustomPrincipal.LoadAsync(authenticationToken.Name);
                    ApplicationContext.User = customPrincipal;
                }
            }
        }
    }
}
