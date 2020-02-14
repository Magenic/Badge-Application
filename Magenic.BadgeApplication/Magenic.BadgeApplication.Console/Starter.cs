using Magenic.BadgeApplication.Common;
using System.Configuration;

namespace Magenic.BadgeApplication.Console
{
    public static class Starter
    {
        public static void Start()
        {
            Logger.Info<Processor.QueueProcessor>($"{nameof(Starter)} initialized.");
            AutofacBootstrapper.Init();
            BadgeSchedulerFactory.StartJob<Processor.QueueProcessor>(ConfigurationManager.AppSettings["QueueCronSchedule"]);
        }
    }
}
