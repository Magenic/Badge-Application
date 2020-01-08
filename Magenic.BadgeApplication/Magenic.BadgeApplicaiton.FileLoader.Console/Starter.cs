using System;
using System.Threading.Tasks;
using Magenic.BadgeApplicaiton.FileLoader.Console;
using Magenic.BadgeApplication.Common;
using System.Threading;

namespace Magenic.BadgeApplication.FileLoader.Console
{
    public static class Starter
    {
        public async static Task Start(bool startAsConsole)
        {
            try
            {
                AutofacBootstrapper.Init();

                var adProcessor = new Processor.FileLoader();
                if (startAsConsole)
                {
                    await adProcessor.StartAsync();
                }
                else
                {
                    _ = Task.Run(() => adProcessor.StartAsync());
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal<Program>(ex.Message, ex);
            }
        }
    }
}
