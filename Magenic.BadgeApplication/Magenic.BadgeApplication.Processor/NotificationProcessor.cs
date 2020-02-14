using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Interfaces;
using Quartz;
using System;
using System.Configuration;

namespace Magenic.BadgeApplication.Processor
{
    [DisallowConcurrentExecution]
    public sealed class NotificationProcessor : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Logger.Info<NotificationProcessor>($"Executing Job {nameof(NotificationProcessor)}.");
                var sendMessageDal = IoC.Container.Resolve<ISendMessageDAL>();
                sendMessageDal.SendActivityNotifications();
                Logger.Info<NotificationProcessor>($"Executed Job {nameof(NotificationProcessor)}.");
            }
            catch (Exception exception)
            {
                Logger.Error<NotificationProcessor>(exception.Message, exception);
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