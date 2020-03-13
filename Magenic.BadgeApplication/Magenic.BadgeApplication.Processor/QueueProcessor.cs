using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using MagenicDataModel;
using Quartz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Magenic.BadgeApplication.Processor
{
    public sealed class QueueProcessor
    {
        private IContainer _factory;

        private IItemProcessor _itemProcessor;
        private IQueueItemDAL _queueItemDAL;
        private IQueueItemToPublishCollectionDAL _queueItemToPublishCollectionDAL;

        private string Environment
        {
            get { return ConfigurationManager.AppSettings["Environment"]; }
        }

        private int SleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["QPSleepIntervalInMilliseconds"]); }
        }

        private int ProcessHourOfDay
        {
            get { return Int32.Parse(ConfigurationManager.AppSettings["QPProcessHourOfDay"]); }
        }

        private int ErrorSleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ErrorSleepIntervalInMilliseconds"]); }
        }

        private string Leaderboard
        {
            get { return ConfigurationManager.AppSettings["LeaderboardURL"]; }
        }

        private string DataService
        {
            get { return ConfigurationManager.AppSettings["ITDataServiceURL"]; }
        }

        public QueueProcessor() : this(IoC.Container)
        {
            
        }

        public QueueProcessor(IContainer factory)
        {
            _factory = factory;

            _itemProcessor = _factory.Resolve<IItemProcessor>();
            _queueItemDAL = _factory.Resolve<IQueueItemDAL>();
            _queueItemToPublishCollectionDAL = _factory.Resolve<IQueueItemToPublishCollectionDAL>();
        }        

        /// <summary>
        /// This method runs the queue process
        /// </summary>
        public void Start()
        {
            var consecutiveErrorCount = 0;
            Logger.Info<QueueProcessor>("The Queue Processor was started");

            while (true)
            {
                try
                {
                    if (ProcessHourOfDay == DateTime.Now.Hour)
                    {
                        var environment = Environment;
                        if (string.IsNullOrWhiteSpace(environment))
                        {
                            environment = "debug";
                        }

                        var itemsToPublish = new List<QueueItemToPublishDTO>();
                        var employees = new List<QueueItemToPublishDTO>();
                        var items = _queueItemToPublishCollectionDAL.GetAllQueueItemsToPublishAsync().Result;
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
                                var employee = context.vwODataEmployees.Where(e => e.EMailAddress == emp.EmailAddress).FirstOrDefault();

                                if (employee != null)
                                {
                                    var empFullName = $"{emp.FirstName} {emp.LastName}";
                                    var adName = emp.ADName.Substring(emp.ADName.IndexOf("\\") + 1);
                                    var empLeaderboardUrl = string.Format(Leaderboard, adName);

                                    var publishMessageConfig = new PublishMessageConfigDTO()
                                    {
                                        Environment = environment,
                                        Title = "Badge Award!",
                                        EmployeeId = emp.EmployeeId,
                                        EmployeeFullName = empFullName,
                                        EmployeeFirstName = emp.FirstName,
                                        EmployeeLastName = emp.LastName,
                                        EmployeeEmailAddress = emp.EmailAddress,
                                        EmployeeADName = emp.ADName,
                                        EmployeeADNameNoDomain = adName,
                                        EmployeeLeaderboard = empLeaderboardUrl,
                                        Leaderboard = Leaderboard,
                                        MagenicDataService = DataService,
                                        QueueItems = new List<PublishQueueItemDTO>()
                                    };

                                    var empBadges = itemsToPublish.Where(x => x.EmployeeId == emp.EmployeeId).ToList();
                                    foreach (var empBadge in empBadges)
                                    {
                                        var publishItem = new PublishQueueItemDTO()
                                        {
                                            QueueItemId = empBadge.QueueItemId,
                                            BadgeAwardId = empBadge.BadgeAwardId,
                                            QueueItemCreated = empBadge.QueueItemCreated,
                                            BadgeId = empBadge.BadgeId,
                                            BadgeName = empBadge.BadgeName,
                                            BadgeTagline = empBadge.BadgeTagline,
                                            BadgeDescription = empBadge.BadgeDescription,
                                            BadgePath = empBadge.BadgePath
                                        };
                                        publishMessageConfig.QueueItems.Add(publishItem);
                                    }

                                    _itemProcessor.ProcessItems(publishMessageConfig);
                                }
                                else
                                {
                                    Logger.Error<QueueProcessor>($"Employee {emp.EmailAddress} does not exist for publishing.");
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
                    consecutiveErrorCount ++;
                    if (consecutiveErrorCount >= 5)
                    {
                        //Continuous logging of an error in a tight loop is bad, go to sleep and see if the system 
                        //recovers
                        Logger.InfoFormat<QueueProcessor>("Queue processor consecutive error limit exceeded, sleeping for {0} seconds", ErrorSleepInterval / 1000);  
                        Thread.Sleep(ErrorSleepInterval);
                    }
                }
            }            
        }
    }
}
