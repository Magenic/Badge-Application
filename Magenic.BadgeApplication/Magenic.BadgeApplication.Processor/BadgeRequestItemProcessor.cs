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
    public class BadgeRequestItemProcessor : IBadgeRequestItemProcessor
    {
        private IContainer _factory;

        private IBadgeRequestDAL _badgeRequestDAL;
        private IEnumerable<IPublisher> _publishers;

        public BadgeRequestItemProcessor() : this(IoC.Container)
        {
        }

        public BadgeRequestItemProcessor(IContainer factory)
        {
            _factory = factory;
            _badgeRequestDAL = _factory.Resolve<IBadgeRequestDAL>();
            _publishers = _factory.Resolve<IEnumerable<IPublisher>>();
        }

        public void ProcessItems(PublishBadgeRequestMsgConfigDTO publishMessageConfig)
        {
            try
            {
                RegisterItemProgress(ProcessingEventType.Processing, publishMessageConfig);

                PublishItems(publishMessageConfig);

                RegisterItemProgress(ProcessingEventType.Processed, publishMessageConfig);
            }
            catch (Exception ex)
            {
                Logger.Error<BadgeRequestItemProcessor>(ex.Message, ex);
                RegisterItemProgress(ProcessingEventType.Failed, publishMessageConfig);
                throw;
            }
        }

        public void PublishItems(PublishBadgeRequestMsgConfigDTO itemsToPublish)
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
                            publisher.PublishBadgeRequest(itemsToPublish);
                        }
                        catch (NotSupportedException)
                        {
                            //ignore error
                        }
                        catch (NotImplementedException)
                        {
                            //ignore error
                        }
                    }
                }
            }
        }

        public void RegisterItemProgress(ProcessingEventType eventType, PublishBadgeRequestMsgConfigDTO publishMessageConfig)
        {
            if (eventType == ProcessingEventType.Processed)
            {
                foreach (var item in publishMessageConfig.BadgeRequestItems)
                {
                    var badgeRequestDTO = new BadgeRequestDTO
                    {
                        BadgeRequestId = item.BadgeRequestId,
                        EmployeeId = item.EmployeeId,
                        BadgeName = item.BadgeName,
                        BadgeDescription = item.BadgeDescription,
                        CreatedDate = item.CreatedDate,
                        NotifySentDate = DateTime.Now
                    };

                    var result = _badgeRequestDAL.Update(badgeRequestDTO);
                }
            }
        }
    }
}
