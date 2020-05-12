using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Processor
{
    public class SubmissionNotifyItemProcessor : ISubmissionNotifyItemProcessor
    {
        private IContainer _factory;

        private INotificationDAL _notificationDAL;
        private IEnumerable<IPublisher> _publishers;

        public SubmissionNotifyItemProcessor() : this(IoC.Container)
        {             
        }

        public SubmissionNotifyItemProcessor(IContainer factory)
        {
            _factory = factory;
            _notificationDAL = _factory.Resolve<INotificationDAL>();
            _publishers = _factory.Resolve<IEnumerable<IPublisher>>();
        }

        public void ProcessItems(PublishNotificationMsgConfigDTO publishMessageConfig)
        {
            try
            {
                RegisterItemProgress(ProcessingEventType.Processing, publishMessageConfig);

                PublishItems(publishMessageConfig);

                RegisterItemProgress(ProcessingEventType.Processed, publishMessageConfig);
            }
            catch (Exception ex)
            {
                Logger.Error<QueueItemProcessor>(ex.Message, ex);
                RegisterItemProgress(ProcessingEventType.Failed, publishMessageConfig);
                throw;
            }
        }

        public void PublishItems(PublishNotificationMsgConfigDTO itemsToPublish)
        {
            var activePublishers = ConfigurationManager.AppSettings["ActivePublishers"];
            if (!string.IsNullOrWhiteSpace(activePublishers))
            {
                foreach (IPublisher publisher in _publishers)
                {
                    if (activePublishers.Contains(publisher.GetType().Name))
                    {
                        try
                        {
                            publisher.PublishSubmitNotify(itemsToPublish);
                        }
                        catch(NotSupportedException)
                        {
                            //ignore error
                        }
                        catch(NotImplementedException)
                        {
                            //ignore error
                        }
                    }
                }
            }
        }

        public void RegisterItemProgress(ProcessingEventType eventType, PublishNotificationMsgConfigDTO publishMessageConfig)
        {
            foreach (var item in publishMessageConfig.NotificationItems)
            {
                var notificationDTO = new NotificationDTO
                {
                    NotificationId = item.NotificationId,
                    ActivitySubmissionId = item.ActivitySubmissionId,
                    CreatedDate = item.CreatedDate,
                    NotificationStatusId = (int)eventType,
                    NotificationSentDate = eventType == ProcessingEventType.Processed ? DateTime.Now : item.NotificationSentDate,
                    UpdatedDate = DateTime.Now
                };

                var result = _notificationDAL.Update(notificationDTO);
            }
        }
    }
}
