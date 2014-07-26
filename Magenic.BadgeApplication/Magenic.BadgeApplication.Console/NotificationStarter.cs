using Magenic.BadgeApplication.Common;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Console
{
    public static class NotificationStarter
    {
        public static void Start()
        {
            try
            {
                AutofacBootstrapper.Init();

                var processor = new Processor.NotificationProcessor();
                Task.Run(() => processor.Start());
            }
            catch (Exception ex)
            {
                Logger.Fatal<Program>(ex.Message, ex);
            }
        }
    }
}
