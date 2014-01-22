using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Threading;

namespace Magenic.BadgeApplication.Processor
{
    public static class QueueProcessor
    {
        /// <summary>
        /// This method begins queue processing.  
        /// </summary>
        public static void Start(IQueueItemDAL queueItemDAL, IQueueEventLogDAL queueEventLogDAL)
        {
            while (true)
            {                 
                QueueItemDTO latestItem = queueItemDAL.Peek();

                if (latestItem != null)
                {
                    IEnumerable<IPublisher> publishers = IoC.Container.Resolve<IEnumerable<IPublisher>>();
                    foreach (IPublisher publiser in publishers)
                    {
                        publiser.Publish();
                    }
                    
                    queueItemDAL.Delete(latestItem.QueueItemId);
                }
                else
                {                    
                    Thread.Sleep(5000);
                }
            }
        }
    }
}
