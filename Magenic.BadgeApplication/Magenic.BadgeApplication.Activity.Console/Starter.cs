using Magenic.BadgeApplication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Activity.Console
{
    public class Starter
    {
        public async static Task Start(bool startAsConsole)
        {
                    try
            {
                AutofacBootstrapper.Init();

                //var adProcessor = new Processor.FileLoader();
                //if (startAsConsole)
                //{
                //    await adProcessor.StartAsync();
                //}
                //else
                //{
                //    Task.Run(() => adProcessor.StartAsync());
                //}
            }
            catch (Exception ex)
            {
                //Logger.Fatal<Program>(ex.Message, ex);
            }
        }
    }
}
