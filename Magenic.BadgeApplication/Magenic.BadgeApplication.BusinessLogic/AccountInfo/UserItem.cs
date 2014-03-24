using System.Globalization;
using Csla;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.BusinessLogic.AccountInfo
{
    public class UserItem : ReadOnlyBase<UserItem>, IUserItem
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

        #endregion Properties

        #region Methods

        internal void Load(UserItemDTO item)
        {
            this.EmployeeId = item.EmployeeId;
            this.EmployeeFirstName = item.EmployeeFirstName;
            this.EmployeeLastName = item.EmployeeLastName;
        }

        #endregion Methods
    }
}
