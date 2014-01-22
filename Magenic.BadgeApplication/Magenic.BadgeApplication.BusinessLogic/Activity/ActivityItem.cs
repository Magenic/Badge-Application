using Csla;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public class ActivityItem : ReadOnlyBase<ActivityItem>, IActivityItem
    {
        #region Properties

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        #endregion Properties

        #region Methods

        internal void Load(ActivityItemDTO item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
        }

        #endregion Methods
    }
}
