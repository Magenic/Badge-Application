namespace Magenic.BadgeApplication.Console
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.QueueProcessorServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.QueueProcessorServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // QueueProcessorServiceProcessInstaller
            // 
            this.QueueProcessorServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.QueueProcessorServiceProcessInstaller.Password = null;
            this.QueueProcessorServiceProcessInstaller.Username = null;
            // 
            // QueueProcessorServiceInstaller
            // 
            this.QueueProcessorServiceInstaller.DisplayName = "Magenic.BadgeApplication.QueueProcessorService";
            this.QueueProcessorServiceInstaller.ServiceName = "Magenic.BadgeApplication.QueueProcessorService";
            this.QueueProcessorServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.QueueProcessorServiceProcessInstaller,
            this.QueueProcessorServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller QueueProcessorServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller QueueProcessorServiceInstaller;
    }
}