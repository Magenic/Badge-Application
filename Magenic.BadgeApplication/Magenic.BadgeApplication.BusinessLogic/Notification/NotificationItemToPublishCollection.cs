using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Notification
{
    /// <summary>
    /// A read only list of notification items.
    /// </summary>
    [Serializable]
    public class NotificationItemToPublishCollection : ReadOnlyListBase<NotificationItemToPublishCollection, INotificationItemToPublish>, INotificationItemToPublishCollection
    {
        #region Factory Methods

        public async static Task<INotificationItemToPublishCollection> GetAllNotificationItemsToPublishAsync()
        {
            return await IoC.Container.Resolve<IObjectFactory<INotificationItemToPublishCollection>>().FetchAsync();
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
            var dal = IoC.Container.Resolve<INotificationItemToPublishCollectionDAL>();

            var result = await dal.GetAllNotificationItemsToPublishAsync();
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<NotificationItemToPublishDTO> data)
        {
            this.IsReadOnly = false;
            foreach (NotificationItemToPublishDTO item in data)
            {
                var newItem = new NotificationItemToPublish();
                newItem.Load(item, true);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }

        #endregion Data Access
    }
}
