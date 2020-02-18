using System;
using System.Configuration;
using System.ServiceProcess;

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
                    var servicesToRun = new ServiceBase[]
                    {
                        new QueueProcessor(),
                        new NotificationProcessor()
                    };
                    ServiceBase.Run(servicesToRun);
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
