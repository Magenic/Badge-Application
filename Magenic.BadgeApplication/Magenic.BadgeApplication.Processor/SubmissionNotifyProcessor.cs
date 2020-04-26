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
using System.Threading;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Processor
{
    public sealed class SubmissionNotifyProcessor
    {
        private IContainer _factory;

        private ISubmissionNotifyItemProcessor _itemProcessor;
        private INotificationDAL _notificationItemDAL;
        private INotificationItemToPublishCollectionDAL _notificationItemToPublishCollectionDAL;

        private string Environment
        {
            get { return ConfigurationManager.AppSettings["Environment"]; }
        }

        private string DataService
        {
            get { return ConfigurationManager.AppSettings["ITDataServiceURL"]; }
        }

        public SubmissionNotifyProcessor() : this(IoC.Container)
        {

        }

        public SubmissionNotifyProcessor(IContainer factory)
        {
            _factory = factory;

            _itemProcessor = _factory.Resolve<ISubmissionNotifyItemProcessor>();
            _notificationItemDAL = _factory.Resolve<INotificationDAL>();
            _notificationItemToPublishCollectionDAL = _factory.Resolve<INotificationItemToPublishCollectionDAL>();
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

                var itemsToPublish = new List<NotificationItemToPublishDTO>();
                var employees = new List<NotificationItemToPublishDTO>();
                var items = _notificationItemToPublishCollectionDAL.GetAllNotificationItemsToPublishAsync().Result;
                if (itemsToPublish != null)
                {
                    foreach (var item in items)
                    {
                        itemsToPublish.Add(item);
                    }
                }

                if (itemsToPublish.Count() > 0)
                {
                    Logger.Info<SubmissionNotifyProcessor>($"SubmissionNotifyProcessor items to process count: {itemsToPublish.Count().ToString()}");

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
                            var adName = emp.ADName.Substring(emp.ADName.IndexOf("\\") + 1);

                            var publishMessageConfig = new PublishNotificationMsgConfigDTO()
                            {
                                Environment = environment,
                                Title = "Activity Submission Notification",
                                EmployeeId = emp.EmployeeId,
                                EmployeeFullName = employee.EmployeeFullName,
                                EmployeeFirstName = employee.EmployeeFirstName,
                                EmployeeLastName = employee.EmployeeLastName,
                                EmployeeEmailAddress = employee.EMailAddress,
                                EmployeeADName = emp.ADName,
                                EmployeeADNameNoDomain = adName,
                                MagenicDataService = DataService,
                                NotificationItems = new List<PublishNotificationItemDTO>()
                            };

                            var empNotifications = itemsToPublish.Where(x => x.EmployeeId == emp.EmployeeId).ToList()
                                                                    .OrderBy(x => x.SubmissionDate).OrderBy(x => x.CreatedDate);
                            ;
                            foreach (var empNotification in empNotifications)
                            {
                                var publishItem = new PublishNotificationItemDTO()
                                {
                                    NotificationId = empNotification.NotificationId,
                                    ActivitySubmissionId = empNotification.ActivitySubmissionId,
                                    CreatedDate = empNotification.CreatedDate,
                                    NotificationStatusId = empNotification.NotificationStatusId,
                                    NotificationSentDate = empNotification.NotificationSentDate,
                                    UpdatedDate = empNotification.UpdatedDate,
                                    ActivityId = empNotification.ActivityId,
                                    ActivityName = empNotification.ActivityName,
                                    ActivityDescription = empNotification.ActivityDescription,
                                    SubmissionDescription = empNotification.SubmissionDescription,
                                    SubmissionApprovedById = empNotification.SubmissionApprovedById,
                                    SubmissionDate = empNotification.SubmissionDate,
                                    SubmissionStatusId = empNotification.SubmissionStatusId,
                                    AwardValue = empNotification.AwardValue
                                };
                                publishMessageConfig.NotificationItems.Add(publishItem);
                            }

                            _itemProcessor.ProcessItems(publishMessageConfig);
                        }
                        else
                        {
                            Logger.Error<SubmissionNotifyProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: Employee {emp.EmailAddress} does not exist for publishing.");
                        }
                    }

                    Logger.Info<SubmissionNotifyProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: SubmissionNotifyProcessor completed");
                }
            }
            catch (Exception ex)
            {
                Logger.Error<SubmissionNotifyProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: {ex.Message}", ex);
            }
        }
    }
}
