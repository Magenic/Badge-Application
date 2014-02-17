using System.Threading.Tasks;
using Magenic.BadgeApplication.Common;
using System;

namespace Magenic.BadgeApplication.Console
{
    public static class Starter
    {
        public static void Start()
        {
            try
            {
                AutofacBootstrapper.Init();

                var processor = new Processor.QueueProcessor();
                Task.Run(() => processor.Start());
            }
            catch (Exception ex)
            {
                Logger.Fatal<Program>(ex.Message, ex);
            }
        }
    }
}
