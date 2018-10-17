using System;
using System.ServiceProcess;

namespace Magenic.BadgeApplication.Console
{
    class Program
    {
        static void Main(string[] args)
        {
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
                    new NotificationProcessor()
                    };
                    ServiceBase.Run(servicesToRun);
                }
                else
                {
                    NotificationStarter.Start();
                    Starter.Start();
                }
            }
        }
    }
}
