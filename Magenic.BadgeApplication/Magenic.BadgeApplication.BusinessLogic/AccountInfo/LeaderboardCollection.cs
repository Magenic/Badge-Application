using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.AccountInfo
{
    /// <summary>
    /// 
    /// </summary>
    public class LeaderboardCollection : ReadOnlyListBase<LeaderboardCollection, ILeaderboardItem>, ILeaderboardCollection
    {
        #region Factory Methods

        /// <summary>
        /// Returns a leaderboard containing a list of users with their badges to enable counts and sorting properly.
        /// </summary>
        /// <returns>A <see cref="ILeaderboardCollection"/> of all users and their badges they have earned.</returns>
        public async static Task<ILeaderboardCollection> GetLeaderboardAsync()
        {
            return await IoC.Container.Resolve<IObjectFactory<ILeaderboardCollection>>().FetchAsync();
        }

        #endregion Factory Methods

        #region Data Access

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private async Task DataPortal_Fetch()
        {
            var dal = IoC.Container.Resolve<ILeaderboardCollectionDAL>();

            var result = await dal.GetLeaderBoardAsync();
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<LeaderboardItemDTO> data)
        {
            this.IsReadOnly = false;
            foreach (LeaderboardItemDTO item in data)
            {
                var newItem = new LeaderboardItem();
                newItem.Load(item);
                this.Add(newItem);
            }
            this.IsReadOnly = true;
        }

        #endregion Data Access
    }
}
