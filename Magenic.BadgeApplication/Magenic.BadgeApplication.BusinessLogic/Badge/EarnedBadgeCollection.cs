using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
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
    public sealed class EarnedBadgeCollection : ReadOnlyListBase<EarnedBadgeCollection, IEarnedBadgeItem>, IEarnedBadgeCollection
    {

        #region Criteria Classes

        [Serializable]
        private class BadgeCollectionForUserCriteria : CriteriaBase<BadgeCollectionForUserCriteria>
        {
            public int EmployeeId { get; set; }
            public BadgeType BadgeType { get; set; }
        }

        #endregion Criteria Classes

        #region Factory Methods

        /// <summary>
        /// Retrieves all earned badges for a user
        /// </summary>
        /// <param name="employeeId">The employee id of the user to get earned badges for.</param>
        /// <param name="badgeType">The type of badge to retrieve.  use unset to get all badge types.</param>
        /// <returns>A collection of earned badges for the provided user of the appropriate type.</returns>
        public async static Task<IEarnedBadgeCollection> GetAllBadgesForUserByTypeAsync(int employeeId, BadgeType badgeType)
        {
            return await IoC.Container.Resolve<IObjectFactory<IEarnedBadgeCollection>>().FetchAsync(new BadgeCollectionForUserCriteria { BadgeType = badgeType, EmployeeId = employeeId });
        }

        #endregion Factory Methods

        #region Data Access

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch(BadgeCollectionForUserCriteria criteria)
        {
            var dal = IoC.Container.Resolve<IEarnedBadgeCollectionDAL>();

            var result = await dal.GetBadgesForUserByBadgeTypeAsync(criteria.EmployeeId, criteria.BadgeType);
            this.LoadData(result);
        }

        internal void LoadData(IEnumerable<EarnedBadgeItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (EarnedBadgeItemDTO item in data)
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
