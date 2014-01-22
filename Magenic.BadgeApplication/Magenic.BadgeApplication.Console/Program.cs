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

                IQueueEventLogDAL queueEventLogDAL = IoC.Container.Resolve<IQueueEventLogDAL>();
                IQueueItemDAL queueItemDAL = IoC.Container.Resolve<IQueueItemDAL>();

                QueueProcessor.Start(queueItemDAL, queueEventLogDAL);

                System.Console.WriteLine("Completed queue processor");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
