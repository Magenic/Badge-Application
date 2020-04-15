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
        public void Process()
        {
            Logger.Info<NotificationProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: The Notification Processor was started");

            try
            {
                var sendMessageDal = IoC.Container.Resolve<ISendMessageDAL>();
                sendMessageDal.SendActivityNotifications();
            }
            catch (Exception ex)
            {
                Logger.Error<NotificationProcessor>(ex.Message, ex);
            }

            Logger.Info<NotificationProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: The Notification Processor completed");
        }
    }
}