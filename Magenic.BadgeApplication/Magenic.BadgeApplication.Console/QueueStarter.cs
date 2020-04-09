using Magenic.BadgeApplication.Common;
using System;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Console
{
    public static class QueueStarter
    {
        public static void Start()
        {
            Logger.Info<Processor.QueueProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: {nameof(QueueStarter)} initialized.");
            AutofacBootstrapper.Init();
            Task.Factory.StartNew(() => new Processor.QueueProcessor().Start());
        }
    }
}
