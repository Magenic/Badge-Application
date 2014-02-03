using Magenic.BadgeApplication.Common;
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
                AutofacBootstrapper.Init();

                QueueProcessor processor = new QueueProcessor();
                processor.Start();             
            }
            catch (Exception ex)
            {
                Logger.Fatal<Program>(ex.Message, ex);
            }
        }
    }
}
