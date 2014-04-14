using Csla;
using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Globalization;

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

        public string FullName
        {
            get { return string.Format(CultureInfo.CurrentCulture, "{0} {1}", EmployeeFirstName, EmployeeLastName); }
        }

        public static readonly PropertyInfo<IEarnedBadgeCollection> EarnedBadgeCollectionProperty = RegisterProperty<IEarnedBadgeCollection>(c => c.EarnedBadges);
        public IEarnedBadgeCollection EarnedBadges
        {
            get { return GetProperty(EarnedBadgeCollectionProperty); }
            private set { LoadProperty(EarnedBadgeCollectionProperty, value); }
        }

        #endregion Properties

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

            var earnedBadgeCollection = new EarnedBadgeCollection();
            earnedBadgeCollection.LoadData(item.EarnedBadges);

            this.EarnedBadges = earnedBadgeCollection;
        }

        #endregion Methods
    }
}
