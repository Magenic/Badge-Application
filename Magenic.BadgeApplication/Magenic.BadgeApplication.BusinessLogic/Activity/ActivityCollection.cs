using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    [Serializable]
    public sealed class ActivityCollection : ReadOnlyListBase<ActivityCollection, IActivityItem>, IActivityCollection
    {
        #region Factory Methods

        public async static Task<IActivityCollection> GetAllActivitiesAsync()
        {
            return await IoC.Container.Resolve<IObjectFactory<IActivityCollection>>().FetchAsync();
        }

        #endregion Factory Methods

        #region Data Access

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch()
        {
            var dal = IoC.Container.Resolve<IActivityCollectionDAL>();

            var result = await dal.GetAllActvitiesAsync();
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<ActivityItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (ActivityItemDTO item in data)
            {
                var newItem = new ActivityItem();
                newItem.Load(item);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }


        #endregion Data Access
    }
}
