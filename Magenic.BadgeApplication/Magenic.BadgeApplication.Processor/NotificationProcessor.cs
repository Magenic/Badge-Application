using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Configuration;
using System.Threading;

namespace Magenic.BadgeApplication.Processor
{
    public class NotificationProcessor
    {
        public void Start()
        {
            var consecutiveErrorCount = 0;

            while (true)
            {
                try
                {
                    if (DateTime.Now.DayOfWeek == NotificationDay && DateTime.Now.Hour == NotificationHourOfDay && DateTime.Now.Minute == 0)
                    {
                        var sendMessageDal = IoC.Container.Resolve<ISendMessageDAL>();
                        sendMessageDal.SendActivityNotifications();
                    }
                    consecutiveErrorCount = 0;
                    consecutiveErrorCount++;
                    if (consecutiveErrorCount >= 5)
                    {
                        //Continuous logging of an error in a tight loop is bad, go to sleep and see if the system 
                        //recovers
                        Logger.InfoFormat<QueueProcessor>("Notification processor consecutive error limit exceeded, sleeping for {0} seconds", ErrorSleepInterval / 1000);
                        Thread.Sleep(ErrorSleepInterval);
                    }

                }
                catch (Exception ex)
                {
                    Logger.Error<ADProcessor>(ex.Message, ex);
                }
                finally
                {
                    Thread.Sleep(SleepInterval);
                }
            }
        }

        private int SleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["SleepIntervalInMilliseconds"]); }
        }

        private int ErrorSleepInterval
        {
            get { return int.Parse(ConfigurationManager.AppSettings["ErrorSleepIntervalInMilliseconds"]); }
        }

        private DayOfWeek NotificationDay
        {
            get { return (DayOfWeek)Enum.Parse(typeof(DayOfWeek), ConfigurationManager.AppSettings["NotificationDay"]); }
        }

        private int NotificationHourOfDay
        {
            get { return Int32.Parse(ConfigurationManager.AppSettings["NotificationHourOfDay"]); }
        }
    }
}
