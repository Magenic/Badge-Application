using Autofac;
using Csla.Security;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Security
{
    [Serializable]
    public sealed class CustomPrincipal : CslaPrincipal
    {
        #region Constructors

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public CustomPrincipal(ICslaIdentity identity)
            : base(identity)
        {
        }

        #endregion Constructors

        #region Factory Methods

        public static async Task<ICslaPrincipal> LoadPrincipalAsync(string userName, string password)
        {
            var criteria = IoC.Container.Resolve<IIdentityCriteria>(new NamedParameter("userName", userName), new NamedParameter("password", password));
            var identity = await IoC.Container.Resolve<IObjectFactory<ICslaIdentity>>().FetchAsync(criteria);
            return IoC.Container.Resolve<ICslaPrincipal>(new NamedParameter("identity", identity));
        }

        #endregion Factory Methods

    }
}