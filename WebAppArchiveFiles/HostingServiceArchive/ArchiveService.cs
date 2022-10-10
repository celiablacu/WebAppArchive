using System.ServiceModel;
using System.ServiceProcess;

namespace Microsoft.ServiceModel.Samples
{

    public partial class ArchiveService : ServiceBase
    {
        public ServiceHost serviceHost = null;

        public ArchiveService()
        {
            ServiceName = "HostingServiceArchive";
        }

        public static void Main()
        {
            ServiceBase.Run(new ArchiveService());
        }
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            serviceHost = new ServiceHost(typeof(WebServiceArchiving));

            // Open the ServiceHostBase to create listeners and start
            // listening for messages.
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
