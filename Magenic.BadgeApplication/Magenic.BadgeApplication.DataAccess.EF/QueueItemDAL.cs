using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Exceptions;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Linq;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class QueueItemDAL : IQueueItemDAL
    {
        public QueueItemDTO Peek()
        {
            using (Entities context = new Entities())
            {
                QueueItem item = context
                    .QueueItems
                    .OrderByDescending(i => i.QueueItemCreated)
                    .FirstOrDefault();
                
                if (item == null)
                {
                    throw new NotFoundException("The top item in the queue could not be found");
                }

                return new QueueItemDTO(item.QueueItemId, item.BadgeAwardId, item.QueueItemCreated);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "QueueItem")]
        public QueueItemDTO Get(int id)
        {
            using (Entities context = new Entities())
            {
                QueueItem item = context
                    .QueueItems
                    .SingleOrDefault(i => i.QueueItemId == id);

                if (item == null)
                {
                    throw new NotFoundException(string.Format("QueueItem with Id {0} could not be found", id));
                }

                return new QueueItemDTO(item.QueueItemId, item.BadgeAwardId, item.QueueItemCreated);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public QueueItemDTO Add(QueueItemDTO item)
        {
            using (Entities context = new Entities())
            {
                QueueItem addedItem = context.QueueItems.Add(new QueueItem 
                {
                    BadgeAwardId = item.BadgeAwardId,
                    QueueItemCreated = DateTime.Now
                });

                context.SaveChanges();

                return new QueueItemDTO(addedItem.QueueItemId, addedItem.BadgeAwardId, addedItem.QueueItemCreated);
            }
        }

        public QueueItemDTO Update(QueueItemDTO item)
        {
            using (Entities context = new Entities())
            {
                QueueItem itemToUpdate = new QueueItem                 
                {
                    QueueItemId = item.QueueItemId,
                    BadgeAwardId = item.BadgeAwardId,                    
                    QueueItemCreated = item.QueueItemCreated
                };

                context.QueueItems.Attach(itemToUpdate);
                context.Entry(itemToUpdate).State = System.Data.Entity.EntityState.Modified;
                
                context.SaveChanges();

                return Get(item.QueueItemId);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "QueueItem")]
        public void Delete(int id)
        {
            using (Entities context = new Entities())
            {
                QueueItem item = context
                    .QueueItems
                    .SingleOrDefault(i => i.QueueItemId == id);

                if (item == null)
                {
                    throw new NotFoundException(string.Format("QueueItem with Id {0} could not be found so it was not deleted", id));
                }

                context.QueueItems.Remove(item);
                context.SaveChanges();
            }
        }
    }
}
