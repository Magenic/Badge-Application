namespace Magenic.BadgeApplicaiton.FileLoader.Console
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
            this.FileLoaderProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.FileLoaderInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // FileLoaderProcessInstaller
            // 
            this.FileLoaderProcessInstaller.Account = System.ServiceProcess.ServiceAccount.NetworkService;
            this.FileLoaderProcessInstaller.Password = null;
            this.FileLoaderProcessInstaller.Username = null;
            // 
            // FileLoaderInstaller
            // 
            this.FileLoaderInstaller.Description = "Loads files into the badge applicaiton.";
            this.FileLoaderInstaller.DisplayName = "Magenic.BadgeApplication.FileLoader";
            this.FileLoaderInstaller.ServiceName = "Magenic.BadgeApplication.FileLoader";
            this.FileLoaderInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.FileLoaderProcessInstaller,
            this.FileLoaderInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller FileLoaderProcessInstaller;
        private System.ServiceProcess.ServiceInstaller FileLoaderInstaller;
    }
}