using Autofac;
using Csla;
using Csla.Rules.CommonRules;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.AccountInfo
{
    [Serializable]
    public sealed class AccountInfoEdit : BusinessBase<AccountInfoEdit>, IAccountInfoEdit
    {
        #region Properties

        public static readonly PropertyInfo<int> EmployeeIdProperty = RegisterProperty<int>(c => c.EmployeeId);
        public int EmployeeId
        {
            get { return GetProperty(EmployeeIdProperty); }
            private set { SetProperty(EmployeeIdProperty, value); }
        }

        public static readonly PropertyInfo<string> UserNameProperty = RegisterProperty<string>(c => c.UserName);
        public string UserName
        {
            get { return GetProperty(UserNameProperty); }
            private set { SetProperty(UserNameProperty, value); }
        }

        public static readonly PropertyInfo<string> EmailAddressProperty = RegisterProperty<string>(c => c.EmailAddress);
        public string EmailAddress
        {
            get { return GetProperty(EmailAddressProperty); }
            private set { SetProperty(EmailAddressProperty, value); }
        }

        public static readonly PropertyInfo<int> TotalPointsEarnedProperty = RegisterProperty<int>(c => c.TotalPointsEarned);
        public int TotalPointsEarned
        {
            get { return GetProperty(TotalPointsEarnedProperty); }
            private set { SetProperty(TotalPointsEarnedProperty, value); }
        }

        public static readonly PropertyInfo<int> TotalPointsPaidOutProperty = RegisterProperty<int>(c => c.TotalPointsPaidOut);
        public int TotalPointsPaidOut
        {
            get { return GetProperty(TotalPointsPaidOutProperty); }
            private set { SetProperty(TotalPointsPaidOutProperty, value); }
        }

        public static readonly PropertyInfo<int> TotalRemainingPointsProperty = RegisterProperty<int>(c => c.TotalRemainingPoints);
        public int TotalRemainingPoints
        {
            get { return (TotalPointsEarned - TotalPointsPaidOut); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static readonly PropertyInfo<int?> PointPayoutThresholdProperty = RegisterProperty<int?>(c => c.PointPayoutThreshold);
        public int? PointPayoutThreshold
        {
            get { return GetProperty(PointPayoutThresholdProperty); }
            set { SetProperty(PointPayoutThresholdProperty, value); }
        }

        #endregion Properties

        #region Factory Methods

        /// <summary>
        /// Asynchronously returns the account information for an employee given a supplied employee id.
        /// </summary>
        /// <param name="employeeId">The employee id to search for.</param>
        /// <returns>The account information for this employee.</returns>
        public static async Task<IAccountInfoEdit> GetAccountInfoForEmployee(int employeeId)
        {
            return await IoC.Container.Resolve<IObjectFactory<IAccountInfoEdit>>().FetchAsync(employeeId);
        }

        #endregion Factory Methods

        #region Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            this.BusinessRules.AddRule(new MinValue<int>(PointPayoutThresholdProperty, 50));
            this.BusinessRules.AddRule(new MaxValue<int>(PointPayoutThresholdProperty, 500));
        }

        #endregion Rules

        #region Data Access

        private async Task DataPortal_Fetch(int employeeId)
        {
            var dal = IoC.Container.Resolve<IAccountInfoEditDAL>();

            var result = await dal.GetAccountInfoByEmployeeIdAsync(employeeId);
            this.LoadData(result);
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Update()
        {
            if (IsDirty)
            {
                var dal = IoC.Container.Resolve<IAccountInfoEditDAL>();
                this.LoadData(dal.Update(this.UnloadData()));
                FieldManager.UpdateChildren();
            }
            this.MarkClean();
            this.MarkOld();
        }

        private AccountInfoEditDTO UnloadData()
        {
            var returnValue = new AccountInfoEditDTO();
            using (this.BypassPropertyChecks)
            {
                returnValue.EmployeeId = this.EmployeeId;
                returnValue.UserName = this.UserName;
                returnValue.EmailAddress = this.EmailAddress;
                returnValue.PointPayoutThreshold = this.PointPayoutThreshold;
                returnValue.TotalPointsEarned = this.TotalPointsEarned;
                returnValue.TotalPointsPaidOut = this.TotalPointsPaidOut;
            }
            return returnValue;
        }

        private void LoadData(AccountInfoEditDTO data)
        {
            using (this.BypassPropertyChecks)
            {
                this.EmployeeId = data.EmployeeId;
                this.UserName = data.UserName;
                this.PointPayoutThreshold = data.PointPayoutThreshold;
                this.TotalPointsEarned = data.TotalPointsEarned;
                this.TotalPointsPaidOut = data.TotalPointsPaidOut;
            }
        }

        #endregion Data Access
    }
}
