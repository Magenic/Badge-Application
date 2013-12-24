using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    /// <summary>
    /// A read only list of earned badge information.
    /// </summary>
    [Serializable]
    public class EarnedBadgeCollection : ReadOnlyListBase<EarnedBadgeCollection, IEarnedBadgeItem>, IEarnedBadgeCollection
    {

        #region Criteria Classes

        [Serializable]
        protected class BadgeCollectionForUserCriteria : CriteriaBase<BadgeCollectionForUserCriteria>
        {
            public string UserName { get; set; }
            public BadgeType BadgeType { get; set; }
        }

        #endregion Criteria Classes

        #region Factory Methods

        /// <summary>
        /// Retrieves all earned badges for a user
        /// </summary>
        /// <param name="userADName">The active directory name of the user to get earned badges for.</param>
        /// <param name="badgeType">The type of badge to retrieve.  use unset to get all badge types.</param>
        /// <returns>A collection of earned badges for the provided user of the appropriate type.</returns>
        public async static Task<IEarnedBadgeCollection> GetAllBadgesForUserByTypeAsync(string userADName, BadgeType badgeType)
        {
            return await IoC.Container.Resolve<IObjectFactory<IEarnedBadgeCollection>>().FetchAsync(new BadgeCollectionForUserCriteria { BadgeType = badgeType, UserName = userADName });
        }

        #endregion Factory Methods

        #region Data Access

        protected async Task DataPortal_Fetch(BadgeCollectionForUserCriteria criteria)
        {
            var dal = IoC.Container.Resolve<IEarnedBadgeCollectionDAL>();

            var result = await dal.GetBadgesForUserByBadgeTypeAsync(criteria.UserName, criteria.BadgeType);
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<IEarnedBadgeItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (IEarnedBadgeItemDTO item in data)
            {
                var newItem = new EarnedBadgeItem();
                newItem.Load(item);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }

        #endregion Data Access
    }
}
