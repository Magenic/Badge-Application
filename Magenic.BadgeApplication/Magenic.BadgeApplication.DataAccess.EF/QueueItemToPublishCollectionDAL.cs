using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class QueueItemToPublishCollectionDAL : IQueueItemToPublishCollectionDAL
    {
        public async Task<IEnumerable<QueueItemToPublishDTO>> GetAllQueueItemsToPublishAsync()
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var items = await (from q in ctx.QueueItemsToPublish
                                   orderby q.QueueItemCreated descending
                                   select new QueueItemToPublishDTO
                                   {
                                       QueueItemId = q.QueueItemId,
                                       BadgeAwardId = q.BadgeAwardId,
                                       QueueItemCreated = q.QueueItemCreated,
                                       BadgeId = q.BadgeId,
                                       BadgeName = q.BadgeName,
                                       BadgeTagline = q.BadgeTagline,
                                       BadgePath = q.BadgePath,
                                       BadgeDescription = q.BadgeDescription,
                                       EmployeeId = q.EmployeeId,
                                       FirstName = q.FirstName,
                                       LastName = q.LastName,
                                       EmailAddress = q.EmailAddress,
                                       ADName = q.ADName
                                   }).ToArrayAsync();

                return items;
            }
        }
    }
}
