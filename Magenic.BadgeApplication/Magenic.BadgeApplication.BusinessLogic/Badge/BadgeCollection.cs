using Autofac;
using Csla;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    /// <summary>
    /// 
    /// </summary>
    public class BadgeCollection
        : BusinessListBase<BadgeCollection, IBadgeReadOnly>, IBadgeCollection
    {
        /// <summary>
        /// The list of badges.
        /// </summary>
        public IEnumerable<IBadgeReadOnly> Badges { get; set; }

        /// <summary>
        /// The fetch portal method.
        /// </summary>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns></returns>
        protected async Task DataPortal_Fetch(BadgeType badgeType)
        {
            var dal = IoC.Container.Resolve<IBadgeReadOnlyDAL>();

            var result = await dal.GetBadgesBadgeTypeAsync(badgeType);
            this.LoadData(result);
        }

        private void LoadData(IEnumerable<IBadgeReadOnlyDTO> data)
        {
            foreach (var item in data)
            {
                var readOnlyItem = new BadgeReadOnly(item.Id, item.Name, item.Type, item.ImagePath);
                this.Add(readOnlyItem);
            }
        }

        /// <summary>
        /// Gets the badges by badge type asynchronous.
        /// </summary>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public async static Task<IEnumerable<IBadgeReadOnly>> GetBadgesByBadgeTypeAsync(BadgeType badgeType)
        {
            return await IoC.Container.Resolve<IObjectFactory<IEnumerable<IBadgeReadOnly>>>().FetchAsync(badgeType);
        }
    }
}
