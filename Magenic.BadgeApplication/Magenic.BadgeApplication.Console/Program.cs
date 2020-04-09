using Magenic.BadgeApplication.Common;
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
                Logger.Info< Program>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: Environment UserInteractive");
                var queueProcessor = new QueueProcessor();
                queueProcessor.RunAsConsole(args);
            }
            else
            {
                if (args.Length == 0)
                {
                    Logger.Info<Program>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: args.length {args.Length.ToString()}");
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
                    Logger.Info<Program>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: NotificationStarter");
                    NotificationStarter.Start();
                    Logger.Info<Program>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: QueueStarter");
                    QueueStarter.Start();
                    Logger.Info<Program>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: SubmissionNotifyStarter");
                    SubmissionNotifyStarter.Start();
                }
            }
        }
    }
}
