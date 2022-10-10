using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Microsoft.ServiceModel.Samples
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    { 

        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            InitializeComponent();

            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "HostingServiceArchive";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
