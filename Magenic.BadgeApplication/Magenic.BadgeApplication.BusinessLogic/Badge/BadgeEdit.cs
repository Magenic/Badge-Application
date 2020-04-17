using Autofac;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
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
    public sealed class BadgeEdit : BusinessBase<BadgeEdit>, IBadgeEdit, ICreateEmployee, IHaveBadgeStatus
    {
        #region Properties

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> TaglineProperty = RegisterProperty<string>(c => c.Tagline);
        public string Tagline
        {
            get { return GetProperty(TaglineProperty); }
            set { SetProperty(TaglineProperty, value); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        public static readonly PropertyInfo<BadgeType> TypeProperty = RegisterProperty<BadgeType>(c => c.Type);
        public BadgeType Type
        {
            get { return GetProperty(TypeProperty); }
            set { SetProperty(TypeProperty, value); }
        }

        public static readonly PropertyInfo<string> ImagePathProperty = RegisterProperty<string>(c => c.ImagePath);
        public string ImagePath
        {
            get { return GetProperty(ImagePathProperty); }
            private set { SetProperty(ImagePathProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> CreatedProperty = RegisterProperty<DateTime>(c => c.Created);
        public DateTime Created
        {
            get { return GetProperty(CreatedProperty); }
            private set { SetProperty(CreatedProperty, value); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static readonly PropertyInfo<DateTime?> EffectiveStartDateProperty = RegisterProperty<DateTime?>(c => c.EffectiveStartDate);
        public DateTime? EffectiveStartDate
        {
            get { return GetProperty(EffectiveStartDateProperty); }
            set { SetProperty(EffectiveStartDateProperty, value); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static readonly PropertyInfo<DateTime?> EffectiveEndDateProperty = RegisterProperty<DateTime?>(c => c.EffectiveEndDate);
        public DateTime? EffectiveEndDate
        {
            get { return GetProperty(EffectiveEndDateProperty); }
            set { SetProperty(EffectiveEndDateProperty, value); }
        }

        public static readonly PropertyInfo<int> PriorityProperty = RegisterProperty<int>(c => c.Priority);
        public int Priority
        {
            get { return GetProperty(PriorityProperty); }
            set { SetProperty(PriorityProperty, value); }
        }

        public static readonly PropertyInfo<bool> MultipleAwardsPossibleProperty = RegisterProperty<bool>(c => c.MultipleAwardsPossible);
        public bool MultipleAwardsPossible
        {
            get { return GetProperty(MultipleAwardsPossibleProperty); }
            set { SetProperty(MultipleAwardsPossibleProperty, value); }
        }

        public static readonly PropertyInfo<bool> DisplayOnceProperty = RegisterProperty<bool>(c => c.DisplayOnce);
        public bool DisplayOnce
        {
            get { return GetProperty(DisplayOnceProperty); }
            set { SetProperty(DisplayOnceProperty, value); }
        }

        public static readonly PropertyInfo<bool> ManagementApprovalRequiredProperty = RegisterProperty<bool>(c => c.ManagementApprovalRequired);
        public bool ManagementApprovalRequired
        {
            get { return GetProperty(ManagementApprovalRequiredProperty); }
            set { SetProperty(ManagementApprovalRequiredProperty, value); }
        }

        public static readonly PropertyInfo<int> ActivityPointsAmountProperty = RegisterProperty<int>(c => c.ActivityPointsAmount);
        public int ActivityPointsAmount
        {
            get { return GetProperty(ActivityPointsAmountProperty); }
            set { SetProperty(ActivityPointsAmountProperty, value); }
        }

        public static readonly PropertyInfo<int> AwardValueAmountProperty = RegisterProperty<int>(c => c.AwardValueAmount);
        public int AwardValueAmount
        {
            get { return GetProperty(AwardValueAmountProperty); }
            set { SetProperty(AwardValueAmountProperty, value); }
        }

        public static readonly PropertyInfo<int> ApprovedByIdProperty = RegisterProperty<int>(c => c.ApprovedById);
        public int ApprovedById
        {
            get { return GetProperty(ApprovedByIdProperty); }
            set { SetProperty(ApprovedByIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime?> ApprovedDateProperty = RegisterProperty<DateTime?>(c => c.ApprovedDate);
        public DateTime? ApprovedDate
        {
            get { return GetProperty(ApprovedDateProperty); }
            private set { SetProperty(ApprovedDateProperty, value); }
        }

        public static readonly PropertyInfo<IBadgeActivityEditCollection> BadgeActivitiesProperty = RegisterProperty<IBadgeActivityEditCollection>(c => c.BadgeActivities);
        public IBadgeActivityEditCollection BadgeActivities
        {
            get { return GetProperty(BadgeActivitiesProperty); }
            private set { SetProperty(BadgeActivitiesProperty, value); }
        }

        public static readonly PropertyInfo<BadgeStatus> BadgeStatusProperty = RegisterProperty<BadgeStatus>(c => c.BadgeStatus);
        public BadgeStatus BadgeStatus
        {
            get { return GetProperty(BadgeStatusProperty); }
            private set { SetProperty(BadgeStatusProperty, value); }
        }

        public static readonly PropertyInfo<int> CreateEmployeeIdProperty = RegisterProperty<int>(c => ((ICreateEmployee)c).CreateEmployeeId);
        int ICreateEmployee.CreateEmployeeId
        {
            get { return GetProperty(CreateEmployeeIdProperty); }
        }

        public static readonly PropertyInfo<byte[]> ImageProperty = RegisterProperty<byte[]>(c => c.Image);
        private byte[] Image
        {
            get { return GetProperty(ImageProperty); }
            set { SetProperty(ImageProperty, value); }
        }

        public static readonly PropertyInfo<bool> AllowDeletionProperty = RegisterProperty<bool>(c => c.AllowDeletion);
        public bool AllowDeletion
        { 
            get { return GetProperty(AllowDeletionProperty); }
            set { SetProperty(AllowDeletionProperty, value); }
        }
        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets a new image for this badge to be saved.  Any existing image path is
        /// cleared out as it is no longer valid.  When the <see cref="BadgeEdit"/> is 
        /// saved, the path to its location will be stored in the image path. 
        /// </summary>
        /// <param name="image">A <see cref="byte"/> array containing the new image.</param>
        public void SetBadgeImage(byte[] image)
        {
            this.LoadProperty(ImageProperty, image);
            this.LoadProperty(ImagePathProperty, string.Empty);
        }
        #endregion Methods

        #region Factory Methods

        public async static Task<IBadgeEdit> GetBadgeEditByIdAsync(int badgeId)
        {
            return await IoC.Container.Resolve<IObjectFactory<IBadgeEdit>>().FetchAsync(badgeId);
        }

        public static IBadgeEdit CreateBadge()
        {
            return IoC.Container.Resolve<IObjectFactory<IBadgeEdit>>().Create();
        }

        #endregion Factory Methods

        #region Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            this.BusinessRules.AddRule(new MaxLength(NameProperty, 100));
            this.BusinessRules.AddRule(new Required(NameProperty));
            this.BusinessRules.AddRule(new MaxLength(TaglineProperty, 200));
            this.BusinessRules.AddRule(new MaxLength(ApprovedByIdProperty, 100));
            this.BusinessRules.AddRule(new DateOrder(EffectiveStartDateProperty, EffectiveEndDateProperty));
            this.BusinessRules.AddRule(new MinValue<int>(ActivityPointsAmountProperty, 1));
            this.BusinessRules.AddRule(new MinValue<int>(CreateEmployeeIdProperty, 1));

            this.BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, ApprovedByIdProperty, PermissionType.Administrator.ToString()));
            this.BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, ApprovedDateProperty, PermissionType.Administrator.ToString()));
            this.BusinessRules.AddRule(new IsInRole(AuthorizationActions.WriteProperty, AwardValueAmountProperty, PermissionType.Administrator.ToString()));

            this.BusinessRules.AddRule(new CanSetBadgeType(AuthorizationActions.WriteProperty, TypeProperty, BadgeType.Corporate, PermissionType.Administrator.ToString()));
            this.BusinessRules.AddRule(new DefaultBadgeStatus(TypeProperty, BadgeStatusProperty, ApprovedByIdProperty));

            this.BusinessRules.AddRule(new ImageProperSize(ImagePathProperty, ImageProperty));
            this.BusinessRules.AddRule(new HasImage(ImagePathProperty, ImageProperty));
        }

        public static void AddObjectAuthorizationRules()
        {
            BusinessRules.AddRule(typeof(IBadgeEdit), new CanChange(AuthorizationActions.DeleteObject, PermissionType.Administrator.ToString()));
            BusinessRules.AddRule(typeof(BadgeEdit), new CanChange(AuthorizationActions.DeleteObject, PermissionType.Administrator.ToString()));
            BusinessRules.AddRule(typeof(IBadgeEdit), new CanChange(AuthorizationActions.EditObject, PermissionType.Administrator.ToString()));
            BusinessRules.AddRule(typeof(BadgeEdit), new CanChange(AuthorizationActions.EditObject, PermissionType.Administrator.ToString()));
        }


        #endregion Rules

        #region Data Access

        [RunLocal]
        protected override void DataPortal_Create()
        {
            base.DataPortal_Create();
            this.LoadProperty(CreatedProperty, DateTime.UtcNow);
            this.LoadProperty(PriorityProperty, int.MaxValue);
            this.LoadProperty(TypeProperty, BadgeType.Corporate);
            this.LoadProperty(BadgeActivitiesProperty, new BadgeActivityEditCollection());
            this.SetProperty(CreateEmployeeIdProperty, ((ICustomPrincipal)ApplicationContext.User).CustomIdentity().EmployeeId);
        }

        private async Task DataPortal_Fetch(int badgeId)
        {
            var dal = IoC.Container.Resolve<IBadgeEditDAL>();

            var result = await dal.GetBadgeByIdAsync(badgeId);
            this.LoadData(result);
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Update()
        {
            if (IsDeleted)
            {
                if (!IsNew)
                {
                    this.DataPortal_DeleteSelf();
                }
                return;
            }

            if (IsNew)
            {
                this.DataPortal_Insert();
            }
            else if (IsDirty)
            {
                var dal = IoC.Container.Resolve<IBadgeEditDAL>();
                this.LoadData(dal.Update(this.UnloadData()));
            }
            this.MarkClean();
            this.MarkOld();
        }

        private BadgeEditDTO UnloadData()
        {
            using (this.BypassPropertyChecks)
            {
                var returnValue = new BadgeEditDTO
                {
                    Id = this.Id,
                    Name = this.Name,
                    Tagline = this.Tagline,
                    Description = this.Description,
                    Type = this.Type,
                    ImagePath = this.ImagePath,
                    Created = this.Created,
                    EffectiveStartDate = this.EffectiveStartDate,
                    EffectiveEndDate = this.EffectiveEndDate,
                    Priority = this.Priority,
                    MultipleAwardsPossible = this.MultipleAwardsPossible,
                    DisplayOnce = this.DisplayOnce,
                    ManagementApprovalRequired = this.ManagementApprovalRequired,
                    ActivityPointsAmount = this.ActivityPointsAmount,
                    AwardValueAmount = this.AwardValueAmount,
                    ApprovedById = this.ApprovedById,
                    ApprovedDate = this.ApprovedDate,
                    BadgeStatus = this.BadgeStatus,
                    BadgeImage = this.Image,
                    CreateEmployeeId = ((ICreateEmployee)this).CreateEmployeeId
                };
                this.UnLoadChildren(returnValue);
                return returnValue;
            }
        }

        private void UnLoadChildren(BadgeEditDTO returnValue)
        {
            returnValue.BadgeActivities = ((BadgeActivityEditCollection)this.BadgeActivities).UnloadChildren();
        }


        private void LoadData(BadgeEditDTO data)
        {
            using (this.BypassPropertyChecks)
            {
                this.Id = data.Id;
                this.Name = data.Name;
                this.Tagline = data.Tagline;
                this.Description = data.Description;
                this.Type = data.Type;
                this.ImagePath = data.ImagePath;
                this.Created = data.Created;
                this.EffectiveStartDate = data.EffectiveStartDate;
                this.EffectiveEndDate = data.EffectiveEndDate;
                this.Priority = data.Priority;
                this.MultipleAwardsPossible = data.MultipleAwardsPossible;
                this.DisplayOnce = data.DisplayOnce;
                this.ManagementApprovalRequired = data.ManagementApprovalRequired;
                this.ActivityPointsAmount = data.ActivityPointsAmount;
                this.AwardValueAmount = data.AwardValueAmount;
                this.ApprovedById = data.ApprovedById;
                this.ApprovedDate = data.ApprovedDate;
                this.BadgeStatus = data.BadgeStatus;
                this.Image = null;
                this.AllowDeletion = !data.HasAwards;
                this.LoadProperty(CreateEmployeeIdProperty, data.CreateEmployeeId);
                this.LoadChildren(data);
            }
        }

        private void LoadChildren(BadgeEditDTO data)
        {
            this.LoadProperty(BadgeActivitiesProperty, new BadgeActivityEditCollection());
            ((BadgeActivityEditCollection)this.BadgeActivities).LoadChildren(data.BadgeActivities);
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_DeleteSelf()
        {
            base.DataPortal_DeleteSelf();
            var dal = IoC.Container.Resolve<IBadgeEditDAL>();

            if (!IsNew)
            {
                this.DeleteChildren();
                dal.Delete(this.Id);
            }

        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void DeleteChildren()
        {
            var dal = IoC.Container.Resolve<IBadgeEditDAL>();
            dal.DeleteActivities(this.Id);
            dal.DeletePrerequisites(this.Id);
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Insert()
        {
            base.DataPortal_Insert();
            var dal = IoC.Container.Resolve<IBadgeEditDAL>();

            this.LoadData(dal.Insert(this.UnloadData()));
            FieldManager.UpdateChildren();
        }

        #endregion Data Access
    }
}
