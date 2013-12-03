using Csla;
using Csla.Rules.CommonRules;
using Csla.Serialization.Mobile;
using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    public class ActivityEdit : BusinessBase<ActivityEdit> , IActivityEdit
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
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        public static readonly PropertyInfo<bool> RequiresApprovalProperty = RegisterProperty<bool>(c => c.RequiresApproval);
        public bool RequiresApproval
        {
            get { return GetProperty(RequiresApprovalProperty); }
            set { SetProperty(RequiresApprovalProperty, value); }
        }

        #endregion Properties

        #region Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            this.BusinessRules.AddRule(new MaxLength(NameProperty, 100));
            this.BusinessRules.AddRule(new Required(NameProperty));
        }

        #endregion Rules
    }
}
