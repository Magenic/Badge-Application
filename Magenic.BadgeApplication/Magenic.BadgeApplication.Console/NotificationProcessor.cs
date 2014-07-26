using System.ServiceProcess;

namespace Magenic.BadgeApplication.Console
{
    partial class NotificationProcessor : ServiceBase
    {
        public NotificationProcessor()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            NotificationStarter.Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
