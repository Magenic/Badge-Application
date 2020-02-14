using System;
using System.Configuration;

namespace Magenic.BadgeApplication.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofacBootstrapper.Init();

            if (Environment.UserInteractive)
            {
                BadgeSchedulerFactory.StartJob<Processor.QueueProcessor>();
            }
            else
            {
                if (args.Length == 0)
                {
                    BadgeSchedulerFactory.StartJob<Processor.NotificationProcessor>(ConfigurationManager.AppSettings["NotificationCronSchedule"]);
                    BadgeSchedulerFactory.StartJob<Processor.QueueProcessor>(ConfigurationManager.AppSettings["QueueCronSchedule"]);
                }
                else
                {
                    BadgeSchedulerFactory.StartJob<Processor.QueueProcessor>();
                    BadgeSchedulerFactory.StartJob<Processor.NotificationProcessor>();
                }
            }
        }
    }
}
