using System.ServiceProcess;
using Magenic.BadgeApplication.Common;
using System;

namespace Magenic.BadgeApplication.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var servicesToRun = new ServiceBase[]
                {
                    new QueueProcessor()
                };
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                Starter.Start();
            }
        }
    }
}
