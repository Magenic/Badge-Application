using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    [Serializable]
    public sealed class ApproveBadgeCollection : ReadOnlyListBase<ApproveBadgeCollection, IApproveBadgeItem>, IApproveBadgeCollection
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

            var result = await dal.GetBadgesToApproveAsync();
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<ApproveBadgeItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (ApproveBadgeItemDTO item in data)
            {
                var newItem = new ApproveBadgeItem();
                newItem.Load(item, true);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }

        #endregion Data Access
    }
}
