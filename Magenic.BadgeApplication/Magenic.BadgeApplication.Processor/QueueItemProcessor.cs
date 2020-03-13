using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

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

        public void ProcessItems(PublishMessageConfigDTO publishMessageConfig)
        {
            try
            {
                RegisterQueueItemProgress(QueueEventType.Processing, publishMessageConfig);

                PublishUpdates(publishMessageConfig);

                _queueItemDAL.DeleteRange(publishMessageConfig.QueueItems.Select(x => x.QueueItemId).ToList());

                RegisterQueueItemProgress(QueueEventType.Processed, publishMessageConfig);
            }
            catch
            {
                RegisterQueueItemProgress(QueueEventType.Failed, publishMessageConfig);
                throw;
            }
        }

        public void PublishUpdates(PublishMessageConfigDTO publishMessageConfig)
        {
            var activePublishers = ConfigurationManager.AppSettings["ActivePublishers"];
            if (!string.IsNullOrWhiteSpace(activePublishers))
            {
                foreach (IPublisher publisher in _publishers)
                {
                    if (activePublishers.Contains(publisher.GetType().Name))
                    {
                        publisher.Publish(publishMessageConfig);
                    }
                }
            }
        }

        public void RegisterQueueItemProgress(QueueEventType eventType, PublishMessageConfigDTO publishMessageConfig)
        {
            foreach(var item in publishMessageConfig.QueueItems)
            {
                QueueEventLogDTO eventLogItem = new QueueEventLogDTO
                {
                    Message = string.Format("Queue Data Item {0} is {1}", item.BadgeAwardId, eventType.ToString()),
                    QueueEventCreated = DateTime.Now,
                    QueueEventId = (int)eventType,
                    BadgeAwardId = item.BadgeAwardId
                };

                var newEventLog = _queueEventLogDAL.Add(eventLogItem);
            }
        }
    }
}
