using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Exceptions;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class NotificationDAL : INotificationDAL
    {
        public NotificationDTO Peek()
        {
            using (Entities context = new Entities())
            {
                Notification item = context
                    .Notifications
                    .OrderByDescending(i => i.CreatedDate)
                    .FirstOrDefault();

                if (item == null)
                {
                    return null;
                }

                var notification = new NotificationDTO()
                {
                    NotificationId = item.NotificationId,
                    ActivitySubmissionId = item.ActivitySubmissionId,
                    CreatedDate = item.CreatedDate,
                    NotificationStatusId = item.NotificationStatusId,
                    NotificationSentDate = item.NotificationSentDate,
                    UpdatedDate = item.UpdatedDate
                };

                return notification;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "QueueItem")]
        public NotificationDTO Get(int id)
        {
            using (Entities context = new Entities())
            {
                Notification item = context
                    .Notifications
                    .SingleOrDefault(i => i.NotificationId == id);

                if (item == null)
                {
                    throw new NotFoundException(string.Format("Notification with Id {0} could not be found", id));
                }

                var notification = new NotificationDTO()
                {
                    NotificationId = item.NotificationId,
                    ActivitySubmissionId = item.ActivitySubmissionId,
                    CreatedDate = item.CreatedDate,
                    NotificationStatusId = item.NotificationStatusId,
                    NotificationSentDate = item.NotificationSentDate,
                    UpdatedDate = item.UpdatedDate
                };

                return notification;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public NotificationDTO Add(NotificationDTO item)
        {
            using (Entities context = new Entities())
            {
                Notification addedItem = context.Notifications.Add(new Notification
                {
                    ActivitySubmissionId = item.ActivitySubmissionId,
                    CreatedDate = DateTime.Now,
                    NotificationStatusId = item.NotificationStatusId,
                    NotificationSentDate = item.NotificationSentDate
                });

                context.SaveChanges();

                var notification = new NotificationDTO()
                {
                    NotificationId = addedItem.NotificationId,
                    ActivitySubmissionId = addedItem.ActivitySubmissionId,
                    CreatedDate = addedItem.CreatedDate,
                    NotificationStatusId = addedItem.NotificationStatusId,
                    NotificationSentDate = addedItem.NotificationSentDate,
                    UpdatedDate = addedItem.UpdatedDate
                };

                return notification;
            }
        }

        public NotificationDTO Update(NotificationDTO item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "NotificationDTO item parameter is null");
            }

            //throw new NotImplementedException();
            using (Entities context = new Entities())
            {
                Notification itemToUpdate = new Notification
                {
                    NotificationId = item.NotificationId,
                    ActivitySubmissionId = item.ActivitySubmissionId,
                    CreatedDate = item.CreatedDate,
                    NotificationStatusId = item.NotificationStatusId,
                    NotificationSentDate = item.NotificationSentDate,
                    UpdatedDate = item.UpdatedDate
                };

                context.Notifications.Attach(itemToUpdate);
                context.Entry(itemToUpdate).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();

                return Get(item.NotificationId);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "QueueItem")]
        public void Delete(int id)
        {
            using (Entities context = new Entities())
            {
                Notification item = context
                    .Notifications
                    .SingleOrDefault(i => i.NotificationId == id);

                if (item == null)
                {
                    throw new NotFoundException(string.Format("QueueItem with Id {0} could not be found so it was not deleted", id));
                }

                context.Notifications.Remove(item);
                context.SaveChanges();
            }
        }

        public void DeleteRange(IList<int> ids)
        {
            if (ids != null && ids.Count() > 0)
            {
                using (Entities context = new Entities())
                {
                    var items = context
                        .Notifications
                        .Where(e => ids.Contains(e.NotificationId));

                    if (items != null && items.Count() > 0)
                    {
                        var itemsToRemove = items.ToList();
                        context.Notifications.RemoveRange(itemsToRemove);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
