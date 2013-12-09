using Autofac;
using Csla;
using Csla.Rules.CommonRules;
using Csla.Serialization.Mobile;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Threading.Tasks;

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

        #region Factory Methods

        public static Task<IActivityEdit> GetActivityEditById(int activityEditId)
        {
            return IoC.Container.Resolve<IDataPortal<IActivityEdit>>().FetchAsync(activityEditId);
        }

        #endregion Factory Methods

        #region Data Access

        private async Task DataPortal_Fetch(int activityEditId)
        {
            var dal = IoC.Container.Resolve<IActivityEditDAL>();

            var result = await dal.GetActivityByIdAsync(activityEditId);
            this.LoadData(result);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        protected new async Task DataPortal_Update()
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
                var dal = IoC.Container.Resolve<IActivityEditDAL>();
                this.LoadData(await dal.UpdateAsync(this.UnloadData()));
                FieldManager.UpdateChildren();
            }
            this.MarkClean();
            this.MarkOld();
        }

        private IActivityEditDTO UnloadData()
        {
            var returnValue = IoC.Container.Resolve<IActivityEditDTO>();
            using (this.BypassPropertyChecks)
            {
                returnValue.Id = this.Id;
                returnValue.Description = this.Description;
                returnValue.Name = this.Name;
                returnValue.RequiresApproval = this.RequiresApproval;
            }
            return returnValue;
        }

        private void LoadData(IActivityEditDTO data)
        {
            using (this.BypassPropertyChecks)
            {
                this.Id = data.Id;
                this.Description = data.Description;
                this.Name = data.Name;
                this.RequiresApproval = data.RequiresApproval;
            }
        }

        #endregion Data Access
    }
}
