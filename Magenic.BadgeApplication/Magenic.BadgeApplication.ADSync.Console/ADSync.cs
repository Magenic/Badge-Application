using System.ServiceProcess;

namespace Magenic.BadgeApplication.ADSync.Console
{
    partial class ADSync : ServiceBase
    {
        public ADSync()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Starter.Start(false);
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
