using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Processor;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.ADSync.Console
{
    public static class Starter
    {
        public static void Start(bool startAsConsole)
        {
            try
            {
                AutofacBootstrapper.Init();

                var adProcessor = new ADProcessor();
                if (startAsConsole)
                {
                    adProcessor.Start();
                }
                else
                {
                    Task.Run(() => adProcessor.Start());
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal<Program>(ex.Message, ex);
            }
        }
    }
}
