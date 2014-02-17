using System.ServiceProcess;

namespace Magenic.BadgeApplication.Console
{
    partial class QueueProcessor : ServiceBase
    {
        public QueueProcessor()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Starter.Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
