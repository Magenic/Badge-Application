using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Exceptions;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Linq;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class QueueEventLogDAL : IQueueEventLogDAL
    {
        public QueueEventLogDTO Get(int id)
        {
            using (Entities context = new Entities())
            {
                QueueEventLog item = context
                    .QueueEventLogs
                    .SingleOrDefault(e => e.QueueEventLogId == id);

                if (item == null)
                {
                    throw new NotFoundException(string.Format("QueueEventLog with Id {0} could not be found", id));
                }

                return new QueueEventLogDTO 
                {
                    Message = item.Message,
                    QueueEventCreated = item.QueueEventCreated,
                    QueueEventId = item.QueueEventId,
                    QueueEventLogId = item.QueueEventLogId,
                    BadgeAwardId = item.BadgeAwardId
                };
            }
        }

        public QueueEventLogDTO Add(QueueEventLogDTO item)
        {
            using (Entities context = new Entities())
            {
                QueueEventLog addedItem = context.QueueEventLogs.Add(
                    new QueueEventLog
                    {
                        Message = item.Message,
                        QueueEventCreated = item.QueueEventCreated,
                        QueueEventId = item.QueueEventId,
                        BadgeAwardId = item.BadgeAwardId
                    });
                
                context.SaveChanges();

                return new QueueEventLogDTO
                {
                    Message = addedItem.Message,
                    QueueEventCreated = addedItem.QueueEventCreated,
                    QueueEventId = addedItem.QueueEventId,
                    QueueEventLogId = addedItem.QueueEventLogId,
                    BadgeAwardId = addedItem.BadgeAwardId
                };
            }
        }

        public QueueEventLogDTO Update(QueueEventLogDTO item)
        {
            using (Entities context = new Entities())
            {
                QueueEventLog itemToUpdate = new QueueEventLog
                {
                    Message = item.Message,
                    QueueEventCreated = item.QueueEventCreated,
                    QueueEventId = item.QueueEventId,
                    QueueEventLogId = item.QueueEventLogId,
                    BadgeAwardId = item.BadgeAwardId
                };

                context.QueueEventLogs.Attach(itemToUpdate);
                context.Entry(itemToUpdate).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();

                return Get(item.QueueEventLogId);
            }
        }

        public void Delete(int id)
        {
            using (Entities context = new Entities())
            {
                QueueEventLog item = context
                    .QueueEventLogs
                    .SingleOrDefault(e => e.QueueEventLogId == id);                    
                    
                if (item == null)
                {
                    throw new NotFoundException(string.Format("QueueEventLog with Id {0} could not be found so it was not deleted", id));
                }

                context.QueueEventLogs.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
