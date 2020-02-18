using System;
using System.Configuration;

namespace Magenic.BadgeApplication.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofacBootstrapper.Init();

            var badgeSchedulerFactory = new BadgeSchedulerFactory();

            if (Environment.UserInteractive)
            {
                badgeSchedulerFactory.StartJob<Processor.QueueProcessor>();
            }
            else
            {
                if (args.Length == 0)
                {
                    badgeSchedulerFactory.StartJob<Processor.NotificationProcessor>(ConfigurationManager.AppSettings["NotificationCronSchedule"]);
                    badgeSchedulerFactory.StartJob<Processor.QueueProcessor>(ConfigurationManager.AppSettings["QueueCronSchedule"]);
                }
                else
                {
                    badgeSchedulerFactory.StartJob<Processor.QueueProcessor>();
                    badgeSchedulerFactory.StartJob<Processor.NotificationProcessor>();
                }
            }
        }
    }
}
