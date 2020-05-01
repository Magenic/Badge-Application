using Autofac;
using Csla;
using Csla.Rules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.BusinessLogic.Rules;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    [Serializable]
    public sealed class EarnedBadgeItem : BusinessBase<EarnedBadgeItem>, IEarnedBadgeItem
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
        /// The description property
        /// </summary>
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(b => b.Description);
        /// <summary>
        /// The description of a badge.
        /// </summary>
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            private set { LoadProperty(DescriptionProperty, value); }
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
            get { return GetProperty(PaidOutProperty); }
            private set { LoadProperty(PaidOutProperty, value); }
        }

        public static readonly PropertyInfo<int> BadgePriorityProperty = RegisterProperty<int>(b => b.BadgePriority);
        /// <summary>
        /// The priority order to display the badges in, lowest to highest.
        /// </summary>
        public int BadgePriority
        {
            get { return GetProperty(BadgePriorityProperty); }
            private set { LoadProperty(BadgePriorityProperty, value); }
        }

        public static readonly PropertyInfo<bool> DisplayOnceProperty = RegisterProperty<bool>(b => b.DisplayOnce);
        /// <summary>
        /// Indicates if the same badge should be displayed only once or multiple times.
        /// </summary>
        public bool DisplayOnce
        {
            get { return GetProperty(DisplayOnceProperty); }
            private set { LoadProperty(DisplayOnceProperty, value); }
        }

        /// <summary>
        /// The identifier for the badgeAward.
        /// </summary>
        public static readonly PropertyInfo<int> BadgeAwardIdProperty = RegisterProperty<int>(b => b.BadgeAwardId);
        /// <summary>
        /// The id of the badge award.
        /// </summary>
        public int BadgeAwardId
        {
            get { return GetProperty(BadgeAwardIdProperty); }
            private set { LoadProperty(BadgeAwardIdProperty, value); }
        }

        /// <summary>
        /// The employee name property
        /// </summary>
        public static readonly PropertyInfo<string> EmployeeNameProperty = RegisterProperty<string>(b => b.EmployeeName);
        /// <summary>
        /// Employee name
        /// </summary>
        public string EmployeeName 
        { 
            get { return GetProperty(EmployeeNameProperty); }
            set { LoadProperty(EmployeeNameProperty, value); }
        }

        /// <summary>
        /// Badge effective end property
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static readonly PropertyInfo<DateTime?> BadgeEffectiveEndProperty = RegisterProperty<DateTime?>(b => b.BadgeEffectiveEnd);
        
        /// <summary>
        /// Badge effective end date
        /// </summary>
        public DateTime? BadgeEffectiveEnd
        { 
            get { return GetProperty(BadgeEffectiveEndProperty); }
            set { LoadProperty(BadgeEffectiveEndProperty, value); }
        }

        /// <summary>
        /// Award amount property
        /// </summary>
        public static readonly PropertyInfo<int> AwardAmountProperty = RegisterProperty<int>(b => b.AwardAmount);
        /// <summary>
        /// Award amount
        /// </summary>
        public int AwardAmount { get; set; }
        #endregion Properties

        #region Methods

        internal void Load(EarnedBadgeItemDTO item)
        {
            this.BadgeAwardId = item.BadgeAwardId;
            this.Id = item.Id;
            this.Name = item.Name;
            this.Description = item.Description;
            this.Type = item.Type;
            this.ImagePath = item.ImagePath;
            this.Tagline = item.Tagline;
            this.AwardDate = item.AwardDate;
            this.AwardPoints = item.AwardPoints;
            this.PaidOut = item.PaidOut;
            this.BadgePriority = item.BadgePriority;
            this.DisplayOnce = item.DisplayOnce;
            this.EmployeeName = item.EmployeeName;
            this.BadgeEffectiveEnd = item.BadgeEffectiveEnd;
            this.AwardAmount = item.AwardAmount;
        }

        public async static Task<IEarnedBadgeItem> GetById(int badgeAwardId)
        {
            return await IoC.Container.Resolve<IObjectFactory<IEarnedBadgeItem>>().FetchAsync(badgeAwardId);
        }
        #endregion Methods

        #region Rules
        public static void AddObjectAuthorizationRules()
        {
            BusinessRules.AddRule(typeof(IEarnedBadgeItem), new CanChange(AuthorizationActions.DeleteObject, PermissionType.Administrator.ToString()));
            BusinessRules.AddRule(typeof(EarnedBadgeItem), new CanChange(AuthorizationActions.DeleteObject, PermissionType.Administrator.ToString()));
        }
        #endregion Rules

        #region Data Access
        private async Task DataPortal_Fetch(int badgeAwardId)
        {
            var dal = IoC.Container.Resolve<IEarnedBadgeCollectionDAL>();

            var result = await dal.GetEarnedBadge(badgeAwardId);
            this.Load(result);
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_DeleteSelf()
        {
            base.DataPortal_DeleteSelf();
            var dal = IoC.Container.Resolve<IEarnedBadgeCollectionDAL>();

            if (!IsNew)
            {
                this.DeleteChildren();
                dal.Delete(this.BadgeAwardId);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void DeleteChildren()
        {
            System.Diagnostics.Debug.WriteLine("TODO: Delete children for earned badges");
            //var dal = IoC.Container.Resolve<IEarnedBadgeCollectionDAL>();
            //dal.DeleteQueueEventLogs(this.Id);
            //dal.DeleteQueueItems(this.Id);
        }
        #endregion Data Access
    }
}
