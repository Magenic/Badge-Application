using Csla;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeReadOnly
        : BusinessBase<BadgeReadOnly>, IBadgeReadOnly
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeReadOnly"/> class.
        /// </summary>
        public BadgeReadOnly()
            : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeReadOnly"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="badgeType">Type of the badge.</param>
        /// <param name="imagePath">The image path.</param>
        public BadgeReadOnly(int id, string name, BadgeType badgeType, string imagePath)
        {
            this.Id = id;
            this.Name = name;
            this.Type = badgeType;
            this.ImagePath = imagePath;
        }

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
            private set { SetProperty(IdProperty, value); }
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
            private set { SetProperty(NameProperty, value); }
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
            private set { SetProperty(TypeProperty, value); }
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
            private set { SetProperty(ImagePathProperty, value); }
        }
    }
}
