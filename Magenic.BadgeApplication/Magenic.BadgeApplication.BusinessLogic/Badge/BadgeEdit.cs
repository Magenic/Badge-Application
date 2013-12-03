using Autofac;
using Csla;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    [Serializable]
    public class BadgeEdit : BusinessBase<BadgeEdit>, IBadgeEdit
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

        public static readonly PropertyInfo<Common.Enums.BadgeType> TypeProperty = RegisterProperty<Common.Enums.BadgeType>(c => c.Type);
        public Common.Enums.BadgeType Type
        {
            get { return GetProperty(TypeProperty); }
            set { SetProperty(TypeProperty, value); }
        }

        public static readonly PropertyInfo<string> ImagePathProperty = RegisterProperty<string>(c => c.ImagePath);
        public string ImagePath
        {
            get { return GetProperty(ImagePathProperty); }
            private set { LoadProperty(ImagePathProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> CreatedProperty = RegisterProperty<DateTime>(c => c.Created);
        public DateTime Created
        {
            get { return GetProperty(CreatedProperty); }
            private set { LoadProperty(CreatedProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EffectiveStartDateProperty = RegisterProperty<DateTime>(c => c.EffectiveStartDate);
        public DateTime EffectiveStartDate
        {
            get { return GetProperty(EffectiveStartDateProperty); }
            set { SetProperty(EffectiveStartDateProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EffectiveEndDateProperty = RegisterProperty<DateTime>(c => c.EffectiveEndDate);
        public DateTime EffectiveEndDate
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

        public static readonly PropertyInfo<DateTime> ApprovedDateProperty = RegisterProperty<DateTime>(c => c.ApprovedDate);
        public DateTime ApprovedDate
        {
            get { return GetProperty(ApprovedDateProperty); }
            private set { LoadProperty(ApprovedDateProperty, value); }
        }

        #endregion Properties

        #region Methods

        public void SetBadgeImage(byte[] image)
        {
            throw new NotImplementedException();
        }

        #endregion Methods

        #region Factory Methods

        public static IBadgeEdit GetBadgeEditById(int badgeId)
        {
            return IoC.Container.Resolve<IObjectFactory<IBadgeEdit>>().Fetch(badgeId);
        }

        public static IBadgeEdit CreateBadgeEdit()
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
        }

        #endregion Rules
    }
}
