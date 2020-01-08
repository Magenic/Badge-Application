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

                var activityLoader = new Processor.ActvityLoader();
                if (startAsConsole)
                {
                    await activityLoader.StartAsync();
                }
                else
                {
                    _ = Task.Run(() => activityLoader.StartAsync());
                }
            }
            catch (Exception ex)
            {
                Logger.Fatal<Program>(ex.Message, ex);
            }
        }
    }
}
