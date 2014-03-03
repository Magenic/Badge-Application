namespace Magenic.BadgeApplication.ADSync.Console
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
            this.ADServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ADServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ADServiceProcessInstaller
            // 
            this.ADServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.ADServiceProcessInstaller.Password = null;
            this.ADServiceProcessInstaller.Username = null;
            // 
            // ADServiceInstaller
            // 
            this.ADServiceInstaller.Description = "Synchronizes AD information to the badge application database.";
            this.ADServiceInstaller.DisplayName = "Magenic.BadgeApplication.ADSync";
            this.ADServiceInstaller.ServiceName = "Magenic.BadgeApplication.ADSync";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ADServiceProcessInstaller,
            this.ADServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ADServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ADServiceInstaller;
    }
}