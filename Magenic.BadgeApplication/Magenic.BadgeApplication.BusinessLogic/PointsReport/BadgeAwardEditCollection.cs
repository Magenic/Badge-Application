using Autofac;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.PointsReport
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeAwardEditCollection
        : ReadOnlyListBase<BadgeAwardEditCollection, IBadgeAwardEdit>, IBadgeAwardEditCollection
    {
        #region Factory Methods

        public async static Task<IBadgeAwardEditCollection> GetAllBadgeAwardsForUser(string userName)
        {
            return await IoC.Container.Resolve<IObjectFactory<IBadgeAwardEditCollection>>().FetchAsync(userName);
        }

        #endregion Factory Methods

        #region Rules

        public static void AddObjectAuthorizationRules()
        {
            BusinessRules.AddRule(typeof(BadgeAwardEditCollection), new IsInRole(AuthorizationActions.GetObject, PermissionType.Administrator.ToString()));
            BusinessRules.AddRule(typeof(BadgeAwardEditCollection), new IsInRole(AuthorizationActions.EditObject, PermissionType.Administrator.ToString()));
        }

        #endregion Rules

        #region Data Access

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch(string userName)
        {
            var dal = IoC.Container.Resolve<IBadgeAwardEditCollectionDAL>();

            var result = await dal.GetAllBadgeAwardsForUserAsync(userName);
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<BadgeAwardEditDTO> data)
        {
            this.IsReadOnly = false;
            foreach (var item in data)
            {
                var newItem = new BadgeAwardEdit();
                newItem.LoadData(item);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }

        #endregion Data Access
    }
}
