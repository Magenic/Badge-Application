using Csla;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public class ApproveActivityBadgeItem : ReadOnlyBase<ApproveActivityBadgeItem>, IApproveActivityBadgeItem
    {
        #region Properties

        public static readonly PropertyInfo<int> BadgeIdProperty = RegisterProperty<int>(c => c.BadgeId);
        public int BadgeId
        {
            get { return GetProperty(BadgeIdProperty); }
            private set { LoadProperty(BadgeIdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<Common.Enums.BadgeType> TypeProperty = RegisterProperty<Common.Enums.BadgeType>(c => c.Type);
        public Common.Enums.BadgeType Type
        {
            get { return GetProperty(TypeProperty); }
            private set { LoadProperty(TypeProperty, value); }
        }

        public static readonly PropertyInfo<string> ImagePathProperty = RegisterProperty<string>(c => c.ImagePath);
        public string ImagePath
        {
            get { return GetProperty(ImagePathProperty); }
            private set { LoadProperty(ImagePathProperty, value); }
        }

        public static readonly PropertyInfo<int> BadgePriorityProperty = RegisterProperty<int>(c => c.BadgePriority);
        public int BadgePriority
        {
            get { return GetProperty(BadgePriorityProperty); }
            private set { LoadProperty(BadgePriorityProperty, value); }
        }

        public static readonly PropertyInfo<int> AwardValueAmountProperty = RegisterProperty<int>(c => c.AwardValueAmount);
        public int AwardValueAmount
        {
            get { return GetProperty(AwardValueAmountProperty); }
            private set { LoadProperty(AwardValueAmountProperty, value); }
        }

        #endregion Properties

        #region Methods

        internal void Load(ApproveActivityBadgeItemDTO item)
        {
            this.BadgeId = item.BadgeId;
            this.Name = item.Name;
            this.Type = item.Type;
            this.ImagePath = item.ImagePath;
            this.BadgePriority = item.BadgePriority;
            this.AwardValueAmount = item.AwardValueAmount;
        }

        #endregion Methods
    }
}
