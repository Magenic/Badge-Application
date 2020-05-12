using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Console
{
    partial class BadgeApplicationService : ServiceBase
    {
        public BadgeApplicationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            BadgeApplicationStarter.Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        internal void RunAsConsole(string[] args)
        {
            OnStart(args);

            System.Console.ReadLine();

            OnStop();

            Thread.Sleep(TimeSpan.FromSeconds(10d));
        }
    }
}
