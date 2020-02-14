using Magenic.BadgeApplication.Common;
using System.Configuration;

namespace Magenic.BadgeApplication.Console
{
    public static class NotificationStarter
    {
        public static void Start()
        {
            Logger.Info<Processor.NotificationProcessor>($"{nameof(NotificationStarter)} initialized.");
            AutofacBootstrapper.Init();
            BadgeSchedulerFactory.StartJob<Processor.NotificationProcessor>(ConfigurationManager.AppSettings["NotificationCronSchedule"]);
        }
    }
}
