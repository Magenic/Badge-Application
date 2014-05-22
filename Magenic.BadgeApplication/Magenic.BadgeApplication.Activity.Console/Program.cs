using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Activity.Console
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var servicesToRun = new ServiceBase[]
                {
                    //new FileLoader(), 
                };
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                Starter.Start(true);
                System.Console.WriteLine("Press any key when done debugging...");

                System.Console.ReadKey(true);
            }
        }
    }
}
