using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Processor;
using System;

namespace Magenic.BadgeApplication.ADSync.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AutofacBootstrapper.Init();

                var adProcessor = new ADProcessor();
                adProcessor.Start();
            }
            catch (Exception ex)
            {
                Logger.Fatal<Program>(ex.Message, ex);
            }
        }
    }
}
