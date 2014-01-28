using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    [Serializable]
    public sealed class ApproveBadgeCollection : BusinessListBase<ApproveBadgeCollection, IApproveBadgeItem>, IApproveBadgeCollection
    {
        
        #region Factory Methods

        /// <summary>
        /// Asynchronously returns a list of all activities awaiting approval for a specific manager.
        /// </summary>
        /// <returns>A list of activities to approve.</returns>
        public async static Task<IApproveBadgeCollection> GetAllBadgesToApproveAsync()
        {
            return await IoC.Container.Resolve<IObjectFactory<IApproveBadgeCollection>>().FetchAsync();
        }

        #endregion Factory Methods

        #region Data Access

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch()
        {
            var dal = IoC.Container.Resolve<IApproveBadgeCollectionDAL>();

            var result = await dal.GetActivitiesToApproveForAdministratorAsync();
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<ApproveBadgeItemDTO> data)
        {
            foreach (ApproveBadgeItemDTO item in data)
            {
                var newItem = new ApproveBadgeItem();
                newItem.Load(item);
                this.Add(newItem);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope, TransactionIsolationLevel.ReadCommitted)]
        protected override void DataPortal_Update()
        {
            var saveList = new List<ApproveBadgeItemDTO>();
            foreach (ApproveBadgeItem i in this)
            {
                saveList.Add(i.Unload());
            }
            var dal = IoC.Container.Resolve<IApproveBadgeCollectionDAL>();
            var results = dal.Update(saveList);
            this.Clear();
            this.DeletedList.Clear();
            this.LoadData(results);
        }

        #endregion Data Access
    }
}
