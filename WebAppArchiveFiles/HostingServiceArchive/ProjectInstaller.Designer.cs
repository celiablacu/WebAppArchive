namespace Microsoft.ServiceModel.Samples
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
            this.ArchivingServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ArchiveHostingService = new System.ServiceProcess.ServiceInstaller();
            // 
            // ArchivingServiceProcessInstaller
            // 
            this.ArchivingServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ArchivingServiceProcessInstaller.Password = null;
            this.ArchivingServiceProcessInstaller.Username = null;
            // 
            // ArchiveHostingService
            // 
            this.ArchiveHostingService.Description = "This service is hosting the Archiving web service";
            this.ArchiveHostingService.DisplayName = "ArchiveHostingService";
            this.ArchiveHostingService.ServiceName = "ArchiveHostingService";
            this.ArchiveHostingService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ArchivingServiceProcessInstaller,
            this.ArchiveHostingService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ArchivingServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ArchiveHostingService;
    }
}