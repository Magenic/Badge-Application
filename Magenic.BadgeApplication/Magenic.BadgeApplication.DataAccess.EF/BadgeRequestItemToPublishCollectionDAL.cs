using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class BadgeRequestItemToPublishCollectionDAL : IBadgeRequestItemToPublishCollectionDAL
    {
        public async Task<IEnumerable<BadgeRequestItemToPublishDTO>> GetAllBadgeRequestItemsToPublishAsync()
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var items = await (from item in ctx.BadgeRequestItemsToPublish.AsNoTracking().Where(x => !x.NotifySentDate.HasValue)
                                   select new BadgeRequestItemToPublishDTO
                                   {
                                       BadgeRequestId = item.BadgeRequestId,
                                       BadgeName = item.BadgeName,
                                       BadgeDescription = item.BadgeDescription,
                                       EmployeeId = item.EmployeeId,
                                       FirstName = item.FirstName,
                                       LastName = item.LastName,
                                       ADName = item.ADName,
                                       EmailAddress = item.EmailAddress,
                                       CreatedDate = item.CreatedDate,
                                       NotifySentDate = item.NotifySentDate
                                   }).ToArrayAsync();

                return items;
            }
        }
    }
}
