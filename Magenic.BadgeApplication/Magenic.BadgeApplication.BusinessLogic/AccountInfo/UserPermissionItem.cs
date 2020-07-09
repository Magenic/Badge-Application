using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.AccountInfo
{
    [Serializable]
    public sealed class UserPermissionItem : BusinessBase<UserPermissionItem>, IUserPermissionItem
    {
        #region Properties
        public static readonly PropertyInfo<int> EmployeePermissionIdProperty = RegisterProperty<int>(c => c.EmployeePermissionId);
        public int EmployeePermissionId
        {
            get { return GetProperty(EmployeePermissionIdProperty); }
            set { LoadProperty(EmployeePermissionIdProperty, value); }
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);
        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
            private set { LoadProperty(FirstNameProperty, value); }
        }

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);
        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
            private set { LoadProperty(LastNameProperty, value); }
        }

        public static readonly PropertyInfo<PermissionType> PermissionIdProperty = RegisterProperty<PermissionType>(c => c.PermissionId);
        public PermissionType PermissionId
        {
            get { return GetProperty(PermissionIdProperty); }
            set { LoadProperty(PermissionIdProperty, value); }
        }
        #endregion

        #region Methods
        public void SetPermissionId(PermissionType permissionId)
        {
            this.SetProperty(PermissionIdProperty, permissionId);
        }

        internal void Load(UserPermissionDTO item)
        {
            this.EmployeePermissionId = item.EmployeePermissionId;
            this.FirstName = item.FirstName;
            this.LastName = item.LastName;
            this.PermissionId = item.PermissionId;
        }

        internal UserPermissionDTO Unload()
        {
            return new UserPermissionDTO 
            { 
                EmployeePermissionId = this.EmployeePermissionId,
                PermissionId = this.PermissionId
            };
        }
        #endregion

        #region Factory Methods
        public static async Task<IUserPermissionItem> GetByIdAsync(int employeePermissionId)
        {
            return await IoC.Container.Resolve<IObjectFactory<IUserPermissionItem>>().FetchAsync(employeePermissionId);
        }
        #endregion

        #region Data Access
        private async Task DataPortal_Fetch(int employeePermissionId)
        {
            var dal = IoC.Container.Resolve<IUserPermissionsDAL>();

            var result = await dal.GetById(employeePermissionId);

            this.Load(result);
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Update()
        {
            var dal = IoC.Container.Resolve<IUserPermissionsDAL>();

            var userPermissionDTO = this.Unload();

            dal.Update(userPermissionDTO);
        }
        #endregion
    }
}
