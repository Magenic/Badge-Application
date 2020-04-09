using Magenic.BadgeApplication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Console
{
    public static class SubmissionNotifyStarter
    {
        public static void Start()
        {
            Logger.Info<Processor.SubmissionNotifyProcessor>($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")}: {nameof(SubmissionNotifyStarter)} initialized.");
            AutofacBootstrapper.Init();
            Task.Factory.StartNew(() => new Processor.SubmissionNotifyProcessor().Start());
        }
    }
}
