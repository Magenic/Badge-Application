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
                var badgeService = new BadgeApplicationService();
                badgeService.RunAsConsole(args);
            }
            else
            {
                if (args.Length == 0)
                {
                    Logger.Info<Program>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: args.length {args.Length.ToString()}");
                    var servicesToRun = new ServiceBase[]
                    {
                        new BadgeApplicationService()
                    };
                    ServiceBase.Run(servicesToRun);
                }
                else
                {
                    Logger.Info<Program>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: BadgeApplicationStarter");
                    BadgeApplicationStarter.Start();
                }
            }
        }
    }
}
