using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Common.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Processor
{
    public sealed class BadgeRequestProcessor
    {
        private IContainer _factory;

        private IBadgeRequestItemProcessor _itemProcessor;
        private IBadgeRequestItemToPublishCollectionDAL _badgeRequestItemToPublishCollectionDAL;

        private string Environment
        {
            get { return ConfigurationManager.AppSettings["Environment"]; }
        }

        private string BadgeRequestsSendTo
        {
            get { return ConfigurationManager.AppSettings["BadgeRequestsSendTo"]; }
        }

        private string DataService
        {
            get { return ConfigurationManager.AppSettings["ITDataServiceURL"]; }
        }

        public BadgeRequestProcessor() : this(IoC.Container)
        {

        }

        public BadgeRequestProcessor(IContainer factory)
        {
            _factory = factory;

            _itemProcessor = _factory.Resolve<IBadgeRequestItemProcessor>();
            _badgeRequestItemToPublishCollectionDAL = _factory.Resolve<IBadgeRequestItemToPublishCollectionDAL>();
        }

        public void Process()
        {
            try
            {
                var environment = Environment;
                if (string.IsNullOrWhiteSpace(environment))
                {
                    environment = "debug";
                }

                var itemsToPublish = new List<BadgeRequestItemToPublishDTO>();
                var employees = new List<BadgeRequestItemToPublishDTO>();
                var items = _badgeRequestItemToPublishCollectionDAL.GetAllBadgeRequestItemsToPublishAsync().Result;
                if (itemsToPublish != null)
                {
                    foreach (var item in items)
                    {
                        itemsToPublish.Add(item);
                    }
                }

                if (itemsToPublish.Count() > 0)
                {
                    Logger.Info<BadgeRequestProcessor>($"BadgeRequestProcessor items to process count: {itemsToPublish.Count().ToString()}");

                    var publishMessageConfig = new PublishBadgeRequestMsgConfigDTO()
                    {
                        Environment = environment,
                        Title = "New Badge Request Notification",
                        SendToEmailAddress = BadgeRequestsSendTo,
                        MagenicDataService = DataService,
                        BadgeRequestItems = new List<PublishBadgeRequestItemDTO>()
                    };

                    employees = itemsToPublish.GroupBy(grp => grp.EmployeeId).Select(g => g.First()).ToList();

                    foreach (var emp in employees)
                    {
                        var dataServiceUri = new Uri(DataService, UriKind.Absolute);
                        var context = new MagenicDataEntities(dataServiceUri)
                        {
                            Credentials = CredentialCache.DefaultCredentials
                        };
                        var employee = context.vwODataEmployees.Where(e => e.NetworkAlias == emp.ADName).FirstOrDefault();

                        if (employee != null)
                        {
                            var empNotifications = itemsToPublish.Where(x => x.EmployeeId == emp.EmployeeId).ToList()
                                                                    .OrderBy(x => x.CreatedDate);

                            foreach (var empNotification in empNotifications)
                            {
                                var adName = empNotification.ADName.Substring(empNotification.ADName.IndexOf("\\") + 1);

                                var publishItem = new PublishBadgeRequestItemDTO()
                                {
                                    ADName = empNotification.ADName,
                                    BadgeDescription = empNotification.BadgeDescription,
                                    BadgeName = empNotification.BadgeName,
                                    BadgeRequestId = empNotification.BadgeRequestId,
                                    CreatedDate = empNotification.CreatedDate,
                                    EmailAddress = empNotification.EmailAddress,
                                    EmployeeId = empNotification.EmployeeId,
                                    FirstName = empNotification.FirstName,
                                    LastName = empNotification.LastName,
                                    FullName = (empNotification.FirstName + " " + empNotification.LastName).Trim(),
                                    ADNameNoDomain = adName,
                                    NotifySentDate = empNotification.NotifySentDate
                                };

                                publishMessageConfig.BadgeRequestItems.Add(publishItem);
                            }
                        }
                        else
                        {
                            Logger.Error<BadgeRequestProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: Employee {emp.EmailAddress} does not exist for publishing.");
                        }
                    }

                    if (publishMessageConfig.BadgeRequestItems.Count() > 0)
                    {
                        _itemProcessor.ProcessItems(publishMessageConfig);
                    }

                    Logger.Info<BadgeRequestProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: BadgeRequestProcessor completed");
                }
            }
            catch (Exception ex)
            {
                Logger.Error<BadgeRequestProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: {ex.Message}", ex);
            }
        }
    }
}
