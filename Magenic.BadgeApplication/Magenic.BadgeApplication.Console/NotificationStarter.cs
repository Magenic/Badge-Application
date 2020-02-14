using Magenic.BadgeApplication.Common;

namespace Magenic.BadgeApplication.Console
{
    public static class NotificationStarter
    {
        public static void Start()
        {
            Logger.Info<Processor.NotificationProcessor>($"{nameof(NotificationStarter)} initialized.");
            AutofacBootstrapper.Init();
            BadgeSchedulerFactory.StartJob<Processor.NotificationProcessor>("0 0 12 ? * MON");
        }
    }
}
