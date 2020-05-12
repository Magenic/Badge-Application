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
    public class NotificationItemToPublishCollectionDAL : INotificationItemToPublishCollectionDAL
    {
        public async Task<IEnumerable<NotificationItemToPublishDTO>> GetAllNotificationItemsToPublishAsync()
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var items = await (from item in ctx.NotificationItemsToPublish.AsNoTracking().Where(x => x.NotificationStatusId == 0)
                                   select new NotificationItemToPublishDTO
                                   {
                                       NotificationId = item.NotificationId,
                                       ActivitySubmissionId = item.ActivitySubmissionId,
                                       CreatedDate = item.CreatedDate,
                                       NotificationStatusId = item.NotificationStatusId,
                                       NotificationSentDate = item.NotificationSentDate,
                                       UpdatedDate = item.UpdatedDate,
                                       ActivityId = item.ActivityId,
                                       EmployeeId = item.EmployeeId,
                                       SubmissionDescription = item.SubmissionDescription,
                                       SubmissionApprovedById = item.SubmissionApprovedById,
                                       SubmissionDate = item.SubmissionDate,
                                       SubmissionStatusId = item.SubmissionStatusId,
                                       AwardValue = item.AwardValue,
                                       ActivityName = item.ActivityName,
                                       ActivityDescription = item.ActivityDescription,
                                       FirstName = item.FirstName,
                                       LastName = item.LastName,
                                       EmailAddress = item.EmailAddress,
                                       ADName = item.ADName
                                   }).ToArrayAsync();

                return items;
            }
        }
    }
}
