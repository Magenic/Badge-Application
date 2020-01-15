using Autofac;
using Csla;
using Csla.Rules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    [Serializable]
    public sealed class BadgeDelete : CommandBase<BadgeDelete>
    {
        #region Properties

        private int badgeAwardId { get; set; }

        #endregion

        #region Factory Methods

        public static async Task DeleteBadgeAsync(int id)
        {
            var badgeDeleteCommand = new BadgeDelete { badgeAwardId = id };
            await IoC.Container.Resolve<IObjectFactory<BadgeDelete>>().ExecuteAsync(badgeDeleteCommand);
        }

        #endregion

        #region Data Access

        protected override void DataPortal_Execute()
        {
            var dal = IoC.Container.Resolve<ILeaderboardItemDAL>();
            dal.Delete(badgeAwardId);
        }

        #endregion

        #region Rules

        internal static void AddObjectAuthorizationRules()
        {
            BusinessRules.AddRule(typeof(BadgeDelete), new Csla.Rules.CommonRules.IsInRole(AuthorizationActions.EditObject, PermissionType.Administrator.ToString()));
        }

        #endregion
    }
}
