using System;

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
                    BadgeSchedulerFactory.StartJob<Processor.NotificationProcessor>("0 0 12 ? * MON");
                    BadgeSchedulerFactory.StartJob<Processor.QueueProcessor>("0/5 * * * * ?");
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
