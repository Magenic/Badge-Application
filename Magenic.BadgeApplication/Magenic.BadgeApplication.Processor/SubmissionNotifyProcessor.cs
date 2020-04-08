using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using MagenicDataModel;
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

        private int SleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["QPSleepIntervalInMilliseconds"]); }
        }

        private string ProcessingHours
        {
            get { return ConfigurationManager.AppSettings["SubmissionNotifyProcessingHours"]; }
        }

        private int ErrorSleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ErrorSleepIntervalInMilliseconds"]); }
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

        public void Start()
        {
            var consecutiveErrorCount = 0;
            var processingHours = new List<int>();

            Logger.Info<SubmissionNotifyProcessor>("The Submission Notify Processor was started");

            while (true)
            {
                try
                {
                    var list = ProcessingHours.ToString().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    foreach(var item in list)
                    {
                        processingHours.Add(int.Parse(item));
                    }

                    if (processingHours.Contains(DateTime.Now.Hour))
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
                                    Logger.Error<SubmissionNotifyProcessor>($"Employee {emp.EmailAddress} does not exist for publishing.");
                                }
                            }
                        }

                        consecutiveErrorCount = 0;
                    }
                    Thread.Sleep(SleepInterval);
                }
                catch (Exception ex)
                {
                    Logger.Error<QueueProcessor>(ex.Message, ex);
                    consecutiveErrorCount++;
                    if (consecutiveErrorCount >= 5)
                    {
                        //Continuous logging of an error in a tight loop is bad, go to sleep and see if the system 
                        //recovers
                        Logger.InfoFormat<SubmissionNotifyProcessor>("Submission Notify processor consecutive error limit exceeded, sleeping for {0} seconds", ErrorSleepInterval / 1000);
                        Thread.Sleep(ErrorSleepInterval);
                    }
                }
            }
        }
    }
}
