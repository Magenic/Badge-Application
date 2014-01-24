﻿using Autofac;
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
    /// A read only list of badge information.
    /// </summary>
    [Serializable]
    public sealed class BadgeCollection : ReadOnlyListBase<BadgeCollection, IBadgeItem>, IBadgeCollection
    {

        #region Criteria Classes

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses"), Serializable]
        internal class BadgeCollectionForUserCriteria : CriteriaBase<BadgeCollectionForUserCriteria>
        {
            public string UserName { get; set; }
            public BadgeType BadgeType { get; set; }
        }

        #endregion Criteria Classes

        #region Factory Methods

        public async static Task<IBadgeCollection> GetAllBadgesByTypeAsync(BadgeType badgeType)
        {
            return await IoC.Container.Resolve<IObjectFactory<IBadgeCollection>>().FetchAsync(badgeType);
        }

        public async static Task<IBadgeCollection> GetAllBadgesForActivitiesAsync(IEnumerable<int> activityIds)
        {
            return await IoC.Container.Resolve<IObjectFactory<IBadgeCollection>>().FetchAsync(activityIds);
        }

        #endregion Factory Methods

        #region Data Access

        /// <summary>
        /// The fetch portal method.
        /// </summary>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch(BadgeType badgeType)
        {
            var dal = IoC.Container.Resolve<IBadgeCollectionDAL>();

            var result = await dal.GetBadgesByBadgeTypeAsync(badgeType);
            this.LoadData(result);
        }

        /// <summary>
        /// The fetch portal method.
        /// </summary>
        /// <param name="activityIds">The activity ids.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch(IEnumerable<int> activityIds)
        {
            var dal = IoC.Container.Resolve<IBadgeCollectionDAL>();

            var result = await dal.GetBadgesByActivityIdsAsync(activityIds);
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<BadgeItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (BadgeItemDTO item in data)
            {
                var newItem = new BadgeItem();
                newItem.Load(item);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }

        #endregion Data Access
    }
}
