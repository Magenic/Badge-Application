using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Queue
{
    /// <summary>
    /// A read only list of queued items.
    /// </summary>
    [Serializable]
    public sealed class QueueItemToPublishCollection : ReadOnlyListBase<QueueItemToPublishCollection, IQueueItemToPublish>, IQueueItemToPublishCollection
    {

        #region Factory Methods

        public async static Task<IQueueItemToPublishCollection> GetAllQueueItemsToPublishAsync()
        {
            return await IoC.Container.Resolve<IObjectFactory<IQueueItemToPublishCollection>>().FetchAsync();
        }

        #endregion Factory Methods

        #region Data Access

        /// <summary>
        /// The fetch portal method.
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch()
        {
            var dal = IoC.Container.Resolve<IQueueItemToPublishCollectionDAL>();

            var result = await dal.GetAllQueueItemsToPublishAsync();
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<QueueItemToPublishDTO> data)
        {
            this.IsReadOnly = false;
            foreach (QueueItemToPublishDTO item in data)
            {
                var newItem = new QueueItemToPublish();
                newItem.Load(item, true);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }

        #endregion Data Access
    }
}
