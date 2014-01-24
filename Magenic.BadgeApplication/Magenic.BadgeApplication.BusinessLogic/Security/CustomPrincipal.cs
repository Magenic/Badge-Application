using Autofac;
using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Security
{
    [Serializable]
    public sealed class CustomPrincipal : CslaPrincipal, ICustomPrincipal
    {
        #region Constructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public CustomPrincipal(ICslaIdentity identity)
            : base(identity)
        {
        }

        #endregion Constructors

        #region Factory Methods

        public static async Task<ICslaPrincipal> LogOnAsync(string userName, string password)
        {
            var criteria = IoC.Container.Resolve<IIdentityCriteria>(new NamedParameter("userName", userName), new NamedParameter("password", password));
            var identity = await IoC.Container.Resolve<IObjectFactory<ICustomIdentity>>().FetchAsync(criteria);

            return IoC.Container.Resolve<ICslaPrincipal>(new NamedParameter("identity", identity));
        }

        public static void LogOff()
        {
            Csla.ApplicationContext.User = new UnauthenticatedPrincipal();
        }

        public static async Task<ICslaPrincipal> LoadAsync(string userName)
        {
            var criteria = IoC.Container.Resolve<IIdentityCriteria>(new NamedParameter("userName", userName), new NamedParameter("password", null));
            var identity = await IoC.Container.Resolve<IObjectFactory<ICustomIdentity>>().FetchAsync(criteria);

            return IoC.Container.Resolve<ICslaPrincipal>(new NamedParameter("identity", identity));
        }

        #endregion Factory Methods

        #region Methods

        /// <summary>
        /// Returns a <see cref="ICustomIdentity"/> for this principal. 
        /// </summary>
        /// <returns>The <see cref="ICustomIdentity"/> for this principal.  Returns 
        /// null if not castable to this type</returns>
        public ICustomIdentity CustomIdentity()
        {
            return this.Identity as ICustomIdentity;
        }
        #endregion Methods

    }
}