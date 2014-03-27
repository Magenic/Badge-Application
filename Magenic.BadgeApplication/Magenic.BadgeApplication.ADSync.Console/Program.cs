using System.ServiceProcess;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Processor;
using System;

namespace Magenic.BadgeApplication.ADSync.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var servicesToRun = new ServiceBase[]
                {
                    new ADSync()
                };
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                Starter.Start(true);
            }
        }
    }
}
