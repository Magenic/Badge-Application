using Magenic.BadgeApplication.FileLoader.Console;
using System.ServiceProcess;

namespace Magenic.BadgeApplicaiton.FileLoader.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var servicesToRun = new ServiceBase[]
                {
                    new FileLoader(), 
                };
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                _ = Starter.Start(true);
                System.Console.WriteLine("Press any key when done debugging...");

                System.Console.ReadKey(true);
            }
        }
    }
}
