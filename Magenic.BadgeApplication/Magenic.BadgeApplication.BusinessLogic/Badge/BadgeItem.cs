using Csla;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    /// <summary>
    /// A read only badge item in a collection.
    /// </summary>
    [Serializable]
    public class BadgeItem : ReadOnlyBase<BadgeItem>, IBadgeItem
    {
        #region Properties

        /// <summary>
        /// The identifier property
        /// </summary>
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(b => b.Id);
        /// <summary>
        /// The id of the badge.
        /// </summary>
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        /// <summary>
        /// The name property
        /// </summary>
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(b => b.Name);
        /// <summary>
        /// The name of a badge.
        /// </summary>
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        /// <summary>
        /// The type property
        /// </summary>
        public static readonly PropertyInfo<BadgeType> TypeProperty = RegisterProperty<BadgeType>(b => b.Type);
        /// <summary>
        /// The type of the badge, corporate or community.
        /// </summary>
        public BadgeType Type
        {
            get { return GetProperty(TypeProperty); }
            private set { LoadProperty(TypeProperty, value); }
        }

        /// <summary>
        /// The image path property
        /// </summary>
        public static readonly PropertyInfo<string> ImagePathProperty = RegisterProperty<string>(b => b.ImagePath);
        /// <summary>
        /// The path to where the badge's image resides.
        /// </summary>
        public string ImagePath
        {
            get { return GetProperty(ImagePathProperty); }
            private set { LoadProperty(ImagePathProperty, value); }
        }

        /// <summary>
        /// The approved date property
        /// </summary>
        public static readonly PropertyInfo<DateTime?> ApprovedDateProperty = RegisterProperty<DateTime?>(b => b.ApprovedDate);
        /// <summary>
        /// The date and time of when this badge was approved to be awarded.
        /// </summary>
        public DateTime? ApprovedDate
        {
            get { return GetProperty(ApprovedDateProperty); }
            private set { LoadProperty(ApprovedDateProperty, value); }
        }

        #endregion Properties

        #region Methods

        internal void Load(IBadgeItemDTO item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.Type = item.Type;
            this.ImagePath = item.ImagePath;
            this.ApprovedDate = item.ApprovedDate;
        }

        #endregion Methods
    }
}
