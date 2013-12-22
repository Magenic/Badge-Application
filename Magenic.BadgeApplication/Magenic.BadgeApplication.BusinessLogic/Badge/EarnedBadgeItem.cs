using Csla;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    [Serializable]
    public class EarnedBadgeItem : ReadOnlyBase<EarnedBadgeItem>, IEarnedBadgeItem
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
        /// The tag line property
        /// </summary>
        public static readonly PropertyInfo<string> TaglineProperty = RegisterProperty<string>(b => b.Tagline);
        /// <summary>
        /// A quip or funny phrase about the badge.
        /// </summary>
        public string Tagline
        {
            get { return GetProperty(TaglineProperty); }
            private set { LoadProperty(TaglineProperty, value); }
        }
        
        /// <summary>
        /// The award date property
        /// </summary>
        public static readonly PropertyInfo<DateTime> AwardDateProperty = RegisterProperty<DateTime>(b => b.AwardDate);
        /// <summary>
        /// The date the badge was awarded.
        /// </summary>
        public DateTime AwardDate
        {
            get { return GetProperty(AwardDateProperty); }
            private set { LoadProperty(AwardDateProperty, value); }
        }
        
        /// <summary>
        /// The award points property
        /// </summary>
        public static readonly PropertyInfo<int> AwardPointsProperty = RegisterProperty<int>(b => b.AwardPoints);
        /// <summary>
        /// The number of points, if any, awarded with this badge.
        /// </summary>
        public int AwardPoints
        {
            get { return GetProperty(AwardPointsProperty); }
            private set { LoadProperty(AwardPointsProperty, value); }
        }

        /// <summary>
        /// The paid out property
        /// </summary>
        public static readonly PropertyInfo<bool> PaidOutProperty = RegisterProperty<bool>(b => b.PaidOut);
        /// <summary>
        /// Indicates if the award points have been paid out.
        /// </summary>
        public bool PaidOut
        {
            get { return GetProperty(PaidOutProperty);  }
            private set { LoadProperty(PaidOutProperty, value); }
        }

        #endregion Properties

        #region Methods

        internal void Load(IEarnedBadgeItemDTO item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.Type = item.Type;
            this.ImagePath = item.ImagePath;
            this.Tagline = item.Tagline;
            this.AwardDate = item.AwardDate;
            this.AwardPoints = item.AwardPoints;
            this.PaidOut = item.PaidOut;
        }

        #endregion Methods
    }
}
