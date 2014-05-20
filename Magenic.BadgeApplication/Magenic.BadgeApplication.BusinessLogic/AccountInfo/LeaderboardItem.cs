using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.AccountInfo
{
    /// <summary>
    /// 
    /// </summary>
    public class LeaderboardItem
        : ReadOnlyBase<LeaderboardItem>, ILeaderboardItem
    {
        #region Properties

        public static readonly PropertyInfo<int> EmployeeIdProperty = RegisterProperty<int>(c => c.EmployeeId);
        public int EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty); }
            private set { LoadProperty(EmployeeIdProperty, value); }
        }

        public static readonly PropertyInfo<string> EmployeeFirstNameProperty = RegisterProperty<string>(c => c.EmployeeFirstName);
        public string EmployeeFirstName
        {
            get { return GetProperty(EmployeeFirstNameProperty); }
            private set { LoadProperty(EmployeeFirstNameProperty, value); }
        }

        public static readonly PropertyInfo<string> EmployeeLastNameProperty = RegisterProperty<string>(c => c.EmployeeLastName);
        public string EmployeeLastName
        {
            get { return GetProperty(EmployeeLastNameProperty); }
            private set { LoadProperty(EmployeeLastNameProperty, value); }
        }

        public static readonly PropertyInfo<string> EmployeeADNameProperty = RegisterProperty<string>(c => c.EmployeeADName);
        public string EmployeeADName
        {
            get { return GetProperty(EmployeeADNameProperty); }
            private set { LoadProperty(EmployeeADNameProperty, value); }
        }

        public static readonly PropertyInfo<string> EmployeeLocationProperty = RegisterProperty<string>(c => c.EmployeeLocation);
        public string EmployeeLocation
        {
            get { return GetProperty(EmployeeLocationProperty); }
            private set { LoadProperty(EmployeeLocationProperty, value); }
        }

        public static readonly PropertyInfo<string> EmployeeDepartmentProperty = RegisterProperty<string>(c => c.EmployeeDepartment);
        public string EmployeeDepartment
        {
            get { return GetProperty(EmployeeDepartmentProperty); }
            private set { LoadProperty(EmployeeDepartmentProperty, value); }
        }

        public static readonly PropertyInfo<IEarnedBadgeCollection> EarnedBadgeCollectionProperty = RegisterProperty<IEarnedBadgeCollection>(c => c.EarnedBadges);
        public IEarnedBadgeCollection EarnedBadges
        {
            get { return GetProperty(EarnedBadgeCollectionProperty); }
            private set { LoadProperty(EarnedBadgeCollectionProperty, value); }
        }

        public string FullName
        {
            get { return string.Format(CultureInfo.CurrentCulture, "{0} {1}", EmployeeFirstName, EmployeeLastName); }
        }

        public IEnumerable<IEarnedBadgeItem> EarnedCorporateBadges
        {
            get
            {
                var badges = BuildEarnedBadges(BadgeType.Corporate);
                return badges.OrderBy(eb => eb.BadgePriority);
            }
        }

        public IEnumerable<IEarnedBadgeItem> EarnedCommunityBadges
        {
            get
            {
                var badges = BuildEarnedBadges(BadgeType.Community);
                return badges.OrderBy(eb => eb.BadgePriority);
            }
        }

        public int EarnedCorporateBadgeCount
        {
            get { return EarnedCorporateBadges.Count(); }
        }

        public int EarnedCommunityBadgeCount
        {
            get { return EarnedCommunityBadges.Count(); }
        }

        #endregion Properties

        #region Fields

        private List<IEarnedBadgeItem> BuildEarnedBadges(BadgeType badgeType)
        {
            var badges = new List<IEarnedBadgeItem>();
            var earnedBadgesOfType = EarnedBadges.Where(eb => eb.Type == badgeType);
            foreach (var earnedBadge in earnedBadgesOfType)
            {
                if (!badges.Where(eb => eb.Id == earnedBadge.Id).Any())
                {
                    badges.Add(earnedBadge);
                }
            }
            return badges;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the name of the leaderboard for user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static async Task<ILeaderboardItem> GetLeaderboardForUserName(string userName)
        {
            return await IoC.Container.Resolve<IObjectFactory<ILeaderboardItem>>().FetchAsync(userName);
        }

        #endregion Factory Methods

        #region Methods

        /// <summary>
        /// Loads the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        internal void Load(LeaderboardItemDTO item)
        {
            this.EmployeeId = item.EmployeeId;
            this.EmployeeFirstName = item.EmployeeFirstName;
            this.EmployeeLastName = item.EmployeeLastName;
            this.EmployeeADName = item.EmployeeADName;
            this.EmployeeLocation = item.EmployeeLocation;
            this.EmployeeDepartment = item.EmployeeDepartment;

            var earnedBadgeCollection = new EarnedBadgeCollection();
            earnedBadgeCollection.LoadData(item.EarnedBadges);

            this.EarnedBadges = earnedBadgeCollection;
        }

        #endregion Methods

        #region Data Access

        private async Task DataPortal_Fetch(string userName)
        {
            var dal = IoC.Container.Resolve<ILeaderboardItemDAL>();
            var result = await dal.GetLeaderboardItemForUserNameAsync(userName);
            this.Load(result);
        }

        #endregion Data Access
    }
}
