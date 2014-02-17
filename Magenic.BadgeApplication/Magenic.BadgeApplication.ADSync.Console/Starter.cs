

using System;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Processor;

namespace Magenic.BadgeApplication.ADSync.Console
{
    public static class Starter
    {
        public static void Start()
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
