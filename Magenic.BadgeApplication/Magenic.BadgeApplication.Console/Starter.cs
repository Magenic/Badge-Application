using Magenic.BadgeApplication.Common;
using System.Configuration;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Console
{
    public static class Starter
    {
        public static void Start()
        {
            Logger.Info<Processor.QueueProcessor>($"{nameof(Starter)} initialized.");
            AutofacBootstrapper.Init();
            Task.Factory.StartNew(() => new Processor.QueueProcessor().Start());
        }
    }
}
