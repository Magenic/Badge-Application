﻿using Autofac;
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
    /// A readonly list of badge information.
    /// </summary>
    [Serializable]
    public class BadgeCollection : ReadOnlyListBase<BadgeCollection, IBadgeItem>, IBadgeCollection
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

        public async static Task<IBadgeCollection> GetAllBadgesByTypeAsync(BadgeType badgeType)
        {
            return await IoC.Container.Resolve<IObjectFactory<IBadgeCollection>>().FetchAsync(badgeType);
        }

        public async static Task<IBadgeCollection> GetAllBadgesForUserByTypeAsync(string userName, BadgeType badgeType)
        {
            return await IoC.Container.Resolve<IObjectFactory<IBadgeCollection>>().FetchAsync(new BadgeCollectionForUserCriteria { BadgeType =  badgeType, UserName =  userName});
        }

        #endregion Factory Methods

        #region Data Access

        /// <summary>
        /// The fetch portal method.
        /// </summary>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns></returns>
        protected async Task DataPortal_Fetch(BadgeType badgeType)
        {
            var dal = IoC.Container.Resolve<IBadgeCollectionDAL>();

            var result = await dal.GetBadgesByBadgeTypeAsync(badgeType);
            this.LoadData(result);
        }

        protected async Task DataPortal_Fetch(BadgeCollectionForUserCriteria criteria)
        {
            var dal = IoC.Container.Resolve<IBadgeCollectionDAL>();

            var result = await dal.GetBadgesForUserByBadgeTypeAsync(criteria.UserName, criteria.BadgeType);
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<IBadgeItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (IBadgeItemDTO item in data)
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