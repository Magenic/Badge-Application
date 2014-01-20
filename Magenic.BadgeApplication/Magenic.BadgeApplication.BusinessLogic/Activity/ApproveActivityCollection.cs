using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public class ApproveActivityCollection : BusinessListBase<ApproveActivityCollection, IApproveActivityItem>, IApproveActivityCollection
    {
        #region Factory Methods

        /// <summary>
        /// Asynchronously returns a list of all activities awaiting approval for a specific manager.
        /// </summary>
        /// <param name="managerEmployeeId">The employee Id of the manager to retrieve all activities for.</param>
        /// <returns>A list of activities to approve.</returns>
        public async static Task<IApproveActivityCollection> GetAllActivitiesToApproveAsync(int managerEmployeeId)
        {
            return await IoC.Container.Resolve<IObjectFactory<IApproveActivityCollection>>().FetchAsync(managerEmployeeId);
        }

        #endregion Factory Methods

        #region Data Access

        protected async Task DataPortal_Fetch(int managerEmployeeId)
        {
            var dal = IoC.Container.Resolve<IApproveActivityCollectionDAL>();

            var result = await dal.GetActivitiesToApproveForManagerAsync(managerEmployeeId);
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<IApproveActivityItemDTO> data)
        {
            foreach (IApproveActivityItemDTO item in data)
            {
                var newItem = new ApproveActivityItem();
                newItem.Load(item);
                this.Add(newItem);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Update()
        {
            var saveList = new List<IApproveActivityItemDTO>();
            foreach (ApproveActivityItem i in this)
            {
                saveList.Add(i.Unload());
            }
            var dal = IoC.Container.Resolve<IApproveActivityCollectionDAL>();
            var results = dal.Update(saveList);
            this.Clear();
            this.DeletedList.Clear();
            this.LoadData(results);
        }


        #endregion Data Access

    }
}
