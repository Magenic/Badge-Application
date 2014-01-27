using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Processor;
using System;

namespace Magenic.BadgeApplication.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                System.Console.WriteLine("Started queue processor");

                AutofacBootstrapper.Init();

                QueueProcessor processor = new QueueProcessor();
                processor.Start();

                System.Console.WriteLine("Completed queue processor");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
