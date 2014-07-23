using Autofac;
using Csla;
using Csla.Rules;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.PointsReport
{
    [Serializable]
    public sealed class BadgeAwardEdit
        : BusinessBase<BadgeAwardEdit>, IBadgeAwardEdit
    {
        #region Properties

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<int> EmployeeIdProperty = RegisterProperty<int>(c => c.EmployeeId);
        public int EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty); }
            private set { SetProperty(EmployeeIdProperty, value); }
        }

        public static readonly PropertyInfo<string> EmployeeADNameProperty = RegisterProperty<string>(c => c.EmployeeADName);
        public string EmployeeADName
        {
            get { return GetProperty(EmployeeADNameProperty); }
            set { SetProperty(EmployeeADNameProperty, value); }
        }

        public static readonly PropertyInfo<int> BadgeIdProperty = RegisterProperty<int>(c => c.BadgeId);
        public int BadgeId
        {
            get { return GetProperty(BadgeIdProperty); }
            private set { SetProperty(BadgeIdProperty, value); }
        }

        public static readonly PropertyInfo<string> BadgeNameProperty = RegisterProperty<string>(c => c.BadgeName);
        public string BadgeName
        {
            get { return GetProperty(BadgeNameProperty); }
            private set { SetProperty(BadgeNameProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> AwardDateProperty = RegisterProperty<DateTime>(c => c.AwardDate);
        public DateTime AwardDate
        {
            get { return GetProperty(AwardDateProperty); }
            private set { SetProperty(AwardDateProperty, value); }
        }

        public static readonly PropertyInfo<int> AwardAmountProperty = RegisterProperty<int>(c => c.AwardAmount);
        public int AwardAmount
        {
            get { return GetProperty(AwardAmountProperty); }
            set { SetProperty(AwardAmountProperty, value); }
        }

        #endregion

        #region Factory Methods

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "activityEdit")]
        public static async Task<IBadgeAwardEdit> GetBadgeAwardEditByIdAsync(int badgeAwardId)
        {
            return await IoC.Container.Resolve<IObjectFactory<IBadgeAwardEdit>>().FetchAsync(badgeAwardId);
        }

        #endregion

        #region Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
        }

        public static void AddObjectAuthorizationRules()
        {
            BusinessRules.AddRule(typeof(IBadgeAwardEdit), new IsInRole(AuthorizationActions.EditObject, PermissionType.Administrator.ToString()));
            BusinessRules.AddRule(typeof(BadgeAwardEdit), new IsInRole(AuthorizationActions.EditObject, PermissionType.Administrator.ToString()));
        }

        #endregion Rules

        #region Data Access

        private async Task DataPortal_Fetch(int badgeAwardEditId)
        {
            var dal = IoC.Container.Resolve<IBadgeAwardEditDAL>();

            var result = await dal.GetBadgeAwardByIdAsync(badgeAwardEditId);
            this.LoadData(result);
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Update()
        {
            if (IsDirty)
            {
                var dal = IoC.Container.Resolve<IBadgeAwardEditDAL>();
                this.LoadData(dal.Update(this.UnloadData()));
                FieldManager.UpdateChildren();
            }
            this.MarkClean();
            this.MarkOld();
        }

        private BadgeAwardEditDTO UnloadData()
        {
            var returnValue = new BadgeAwardEditDTO();
            using (this.BypassPropertyChecks)
            {
                returnValue.Id = this.Id;
                returnValue.EmployeeId = this.EmployeeId;
                returnValue.EmployeeADName = this.EmployeeADName;
                returnValue.BadgeId = this.BadgeId;
                returnValue.BadgeName = this.BadgeName;
                returnValue.AwardDate = this.AwardDate;
                returnValue.AwardAmount = this.AwardAmount;
            }
            return returnValue;
        }

        internal void LoadData(BadgeAwardEditDTO item)
        {
            using (this.BypassPropertyChecks)
            {
                this.Id = item.Id;
                this.EmployeeId = item.EmployeeId;
                this.EmployeeADName = item.EmployeeADName;
                this.BadgeId = item.BadgeId;
                this.BadgeName = item.BadgeName;
                this.AwardDate = item.AwardDate;
                this.AwardAmount = item.AwardAmount;
            }
        }

        #endregion
    }
}
