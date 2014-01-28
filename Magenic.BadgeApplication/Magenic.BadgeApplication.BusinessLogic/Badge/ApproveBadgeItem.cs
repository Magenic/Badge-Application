using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    [Serializable]
    public sealed class ApproveBadgeItem : BusinessBase<ApproveBadgeItem>, IApproveBadgeItem
    {
        #region Properties

        public static readonly PropertyInfo<int> BadgeIdProperty = RegisterProperty<int>(c => c.BadgeId);
        public int BadgeId
        {
            get { return GetProperty(BadgeIdProperty); }
            private set { SetProperty(BadgeIdProperty, value); }
        }


        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> TaglineProperty = RegisterProperty<string>(c => c.Tagline);
        public string Tagline
        {
            get { return GetProperty(TaglineProperty); }
            private set { SetProperty(TaglineProperty, value); }
        }
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            private set { SetProperty(DescriptionProperty, value); }
        }

        public static readonly PropertyInfo<BadgeType> TypeProperty = RegisterProperty<BadgeType>(c => c.Type);
        public BadgeType Type
        {
            get { return GetProperty(TypeProperty); }
            private set { SetProperty(TypeProperty, value); }
        }

        public static readonly PropertyInfo<string> ImagePathProperty = RegisterProperty<string>(c => c.ImagePath);
        public string ImagePath
        {
            get { return GetProperty(ImagePathProperty); }
            private set { LoadProperty(ImagePathProperty, value); }
        }

        public static readonly PropertyInfo<int> AwardValueAmountProperty = RegisterProperty<int>(c => c.AwardValueAmount);
        public int AwardValueAmount
        {
            get { return GetProperty(AwardValueAmountProperty); }
            private set { SetProperty(AwardValueAmountProperty, value); }
        }

        public static readonly PropertyInfo<int> ApprovedByIdProperty = RegisterProperty<int>(c => c.ApprovedById);
        public int ApprovedById
        {
            get { return GetProperty(ApprovedByIdProperty); }
            private set { SetProperty(ApprovedByIdProperty, value); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static readonly PropertyInfo<DateTime?> ApprovedDateProperty = RegisterProperty<DateTime?>(c => c.ApprovedDate);
        public DateTime? ApprovedDate
        {
            get { return GetProperty(ApprovedDateProperty); }
            private set { LoadProperty(ApprovedDateProperty, value); }
        }

        public static readonly PropertyInfo<BadgeStatus> BadgeStatusProperty = RegisterProperty<BadgeStatus>(c => c.BadgeStatus);
        public BadgeStatus BadgeStatus
        {
            get { return GetProperty(BadgeStatusProperty); }
            private set { LoadProperty(BadgeStatusProperty, value); }
        }
        #endregion Properties

        #region Methods

        public static readonly MethodInfo ApproveBadgeMethod = RegisterMethod(typeof(ApproveBadgeItem), "ApproveBadge");

        public void ApproveBadge(int approverUserId)
        {
            if (CanExecuteMethod(ApproveBadgeMethod))
            {
                this.BadgeStatus = BadgeStatus.Approved;
                this.ApprovedById = approverUserId;
                this.ApprovedDate = DateTime.UtcNow;
            }
        }

        public static readonly MethodInfo DenyBadgeMethod = RegisterMethod(typeof(ApproveBadgeItem), "DenyBadge");
        public void DenyBadge()
        {
            if (CanExecuteMethod(DenyBadgeMethod))
            {
                this.BadgeStatus = BadgeStatus.Denied;
                this.ApprovedById = 0;
                this.ApprovedDate = null;
            }
        }

        #endregion Methods

        #region Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            this.BusinessRules.AddRule(new IsInRole(AuthorizationActions.ExecuteMethod, ApproveBadgeMethod, PermissionType.Administrator.ToString()));
            this.BusinessRules.AddRule(new IsInRole(AuthorizationActions.ExecuteMethod, DenyBadgeMethod, PermissionType.Administrator.ToString()));
        }

        #endregion Rules

        internal ApproveBadgeItemDTO Unload()
        {
            using (this.BypassPropertyChecks)
            {
                var returnValue = new ApproveBadgeItemDTO
                {
                    BadgeId = this.BadgeId,
                    Name = this.Name,
                    Description = this.Description,
                    Tagline = this.Tagline,
                    Type = this.Type,
                    AwardValueAmount = this.AwardValueAmount,
                    ImagePath = this.ImagePath,
                    BadgeStatus = this.BadgeStatus,
                    ApprovedById = this.ApprovedById,
                    ApprovedDate = this.ApprovedDate
                };
                return returnValue;
            }
        }

        internal void Load(ApproveBadgeItemDTO data)
        {
            using (this.BypassPropertyChecks)
            {
                this.BadgeId = data.BadgeId;
                this.Name = data.Name;
                this.Description = data.Description;
                this.Tagline = data.Tagline;
                this.Type = data.Type;
                this.AwardValueAmount = data.AwardValueAmount;
                this.ImagePath = data.ImagePath;
                this.BadgeStatus = data.BadgeStatus;
                this.ApprovedById = data.ApprovedById;
                this.ApprovedDate = data.ApprovedDate;
            }
            this.MarkClean();
            this.MarkOld();
            this.MarkAsChild();
        }
    }
}
