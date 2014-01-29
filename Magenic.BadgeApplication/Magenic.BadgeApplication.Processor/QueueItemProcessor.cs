using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace Magenic.BadgeApplication.Processor
{
    /// <summary>
    /// 
    /// </summary>
    public class QueueItemProcessor : IItemProcessor
    {
        private IContainer _factory;

        private IQueueItemDAL _queueItemDAL;
        private IQueueEventLogDAL _queueEventLogDAL;
        private IEarnedBadgeCollectionDAL _earnedBadgeDAL;
        private IEnumerable<IPublisher> _publishers;

        public QueueItemProcessor() : this(IoC.Container)
        {             
        }

        public QueueItemProcessor(IContainer factory)
        {
            _factory = factory;

            _earnedBadgeDAL = _factory.Resolve<IEarnedBadgeCollectionDAL>();
            _queueItemDAL = _factory.Resolve<IQueueItemDAL>();
            _queueEventLogDAL = _factory.Resolve<IQueueEventLogDAL>();
            _publishers = _factory.Resolve<IEnumerable<IPublisher>>();
        }

        public void ProcessItem(QueueItemDTO latestItem)
        {
            try
            {
                EarnedBadgeItemDTO earnedBadge = _earnedBadgeDAL.GetEarnedBadge(latestItem.BadgeAwardId);

                RegisterQueueItemProgress(QueueEventType.Processing, latestItem);

                PublishUpdates(earnedBadge);

                _queueItemDAL.Delete(latestItem.QueueItemId);

                RegisterQueueItemProgress(QueueEventType.Processed, latestItem);
            }
            catch
            {
                RegisterQueueItemProgress(QueueEventType.Failed, latestItem);
                throw;
            }
        }

        public void PublishUpdates(EarnedBadgeItemDTO earnedBadge)
        {
            foreach (IPublisher publiser in _publishers)
            {
                publiser.Publish(earnedBadge);
            }
        }

        public void RegisterQueueItemProgress(QueueEventType eventType, QueueItemDTO latestItem)
        {
            QueueEventLogDTO eventLogItem = new QueueEventLogDTO
            {
                Message = string.Format("Queue Data Item {0} is {1}", latestItem.BadgeAwardId, eventType.ToString()),
                QueueEventCreated = DateTime.Now,
                QueueEventId = (int)eventType,
                BadgeAwardId = latestItem.BadgeAwardId
            };

            _queueEventLogDAL.Add(eventLogItem);
        }
    }
}
