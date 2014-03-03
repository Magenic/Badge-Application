using Csla;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public sealed class ActivityItem : ReadOnlyBase<ActivityItem>, IActivityItem
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

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "We should figure out a better way of doing this")]
        public static readonly PropertyInfo<IEnumerable<int>> BadgeIdsProperty = RegisterProperty<IEnumerable<int>>(c => c.BadgeIds);
        public IEnumerable<int> BadgeIds
        {
            get { return GetProperty(BadgeIdsProperty); }
            private set { LoadProperty(BadgeIdsProperty, value); }
        }

        #endregion Properties

        #region Methods

        internal void Load(ActivityItemDTO item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.BadgeIds = item.BadgeIds;
        }

        #endregion Methods
    }
}
