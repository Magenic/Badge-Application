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

        private IQueueItemProcessor _itemProcessor;
        private IQueueItemDAL _queueItemDAL;
        private IQueueItemToPublishCollectionDAL _queueItemToPublishCollectionDAL;

        private string Environment
        {
            get { return ConfigurationManager.AppSettings["Environment"]; }
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

            _itemProcessor = _factory.Resolve<IQueueItemProcessor>();
            _queueItemDAL = _factory.Resolve<IQueueItemDAL>();
            _queueItemToPublishCollectionDAL = _factory.Resolve<IQueueItemToPublishCollectionDAL>();
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
                    Logger.Info<QueueProcessor>($"QueueProcessor items to process count: {itemsToPublish.Count().ToString()}");

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
                            var empLeaderboardUrl = string.Format(Leaderboard, adName);

                            var publishMessageConfig = new PublishBadgeMsgConfigDTO()
                            {
                                Environment = environment,
                                Title = "Badge Award!",
                                EmployeeId = emp.EmployeeId,
                                EmployeeFullName = employee.EmployeeFullName,
                                EmployeeFirstName = employee.EmployeeFirstName,
                                EmployeeLastName = employee.EmployeeLastName,
                                EmployeeEmailAddress = employee.EMailAddress,
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
                            Logger.Error<QueueProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: Employee {emp.EmailAddress} does not exist for publishing.");
                        }
                    }
                
                    Logger.Info<QueueProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: QueueProcessor completed");
                }
            }
            catch (Exception ex)
            {
                Logger.Error<QueueProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: {ex.Message}", ex);
            }
        }
    }
}
