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

            if (Environment.UserInteractive)
            {
                var queueProcessor = new QueueProcessor();
                queueProcessor.RunAsConsole(args);
            }
            else
            {
                if (args.Length == 0)
                {
                    var servicesToRun = new ServiceBase[]
                    {
                    new QueueProcessor(),
                    new NotificationProcessor(),
                    new SubmissionNotifyProcessor()
                    };
                    ServiceBase.Run(servicesToRun);
                }
                else
                {
                    NotificationStarter.Start();
                    QueueStarter.Start();
                    SubmissionNotifyStarter.Start();
                }
            }
        }
    }
}
