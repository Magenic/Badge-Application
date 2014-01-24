using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace Magenic.BadgeApplication.Processor
{
    public static class QueueProcessor
    {
        private static bool _isStopRequested = false;

        private static int SleepInterval 
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SleepIntervalInMilliseconds"]); }    
        }

        /// <summary>
        /// This method runs the queue process
        /// </summary>
        public static void Start(IQueueItemDAL queueItemDAL, IQueueEventLogDAL queueEventLogDAL)
        {
            while (true)
            {
                try
                {
                    QueueItemDTO latestItem = queueItemDAL.Peek();

                    if (latestItem != null)
                    {
                        ProcessItem(latestItem, queueEventLogDAL, queueItemDAL);
                    }
                    else
                    {
                        Thread.Sleep(SleepInterval);
                    }

                    if (_isStopRequested)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void ProcessItem(QueueItemDTO latestItem, IQueueEventLogDAL queueEventLogDAL, IQueueItemDAL queueItemDAL)
        {
            try
            {
                EarnedBadgeItemDTO earnedBadge = GetEarnedBadge(latestItem);

                RegisterQueueItemProgress(QueueEventType.Processing, latestItem, queueEventLogDAL);

                PublishUpdates(earnedBadge);

                queueItemDAL.Delete(latestItem.QueueItemId);

                RegisterQueueItemProgress(QueueEventType.Processed, latestItem, queueEventLogDAL);
            }
            catch
            {
                RegisterQueueItemProgress(QueueEventType.Failed, latestItem, queueEventLogDAL);
                throw;
            }
        }

        private static void PublishUpdates(EarnedBadgeItemDTO earnedBadge)
        {
            IEnumerable<IPublisher> publishers = IoC.Container.Resolve<IEnumerable<IPublisher>>();
            foreach (IPublisher publiser in publishers)
            {
                publiser.Publish(earnedBadge);
            }
        }

        private static EarnedBadgeItemDTO GetEarnedBadge(QueueItemDTO latestItem)
        {
            IEarnedBadgeCollectionDAL earnedBadgeDAL = IoC.Container.Resolve<IEarnedBadgeCollectionDAL>();
            EarnedBadgeItemDTO earnedBadge = earnedBadgeDAL.GetEarnedBadge(latestItem.BadgeAwardId);
            return earnedBadge;
        }

        private static void RegisterQueueItemProgress(QueueEventType eventType, QueueItemDTO latestItem, IQueueEventLogDAL queueEventLogDAL)
        {
            QueueEventLogDTO eventLogItem = new QueueEventLogDTO
            {
                Message = string.Format("Queue Data Item {0} is {1}", eventType.ToString()),
                QueueEventCreated = DateTime.Now,
                QueueEventId = (int)eventType,
                QueueItemId = latestItem.QueueItemId
            };

            queueEventLogDAL.Add(eventLogItem);
        }
    }
}
