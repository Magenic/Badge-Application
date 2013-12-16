using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Activity
{
    public class ActivityCollection : ReadOnlyListBase<ActivityCollection, IActivityItem>, IActivityCollection
    {
        #region Factory Methods

        public async static Task<IActivityCollection> GetAllActivitiesAsync()
        {
            return await IoC.Container.Resolve<IObjectFactory<IActivityCollection>>().FetchAsync();
        }

        #endregion Factory Methods

        #region Data Access

        protected async Task DataPortal_Fetch()
        {
            var dal = IoC.Container.Resolve<IActivityCollectionDAL>();

            var result = await dal.GetAllActvitiesAsync();
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<IActivityItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (IActivityItemDTO item in data)
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
