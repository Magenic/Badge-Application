using Magenic.BadgeApplication.Common;
using System;
using System.Threading.Tasks;

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
                Task.Factory.StartNew(() => processor.Start());
            }
            catch (Exception ex)
            {
                Logger.Fatal<Program>(ex.Message, ex);
                throw;
            }
        }
    }
}
