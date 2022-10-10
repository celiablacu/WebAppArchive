using System.ServiceModel;

namespace Microsoft.ServiceModel.Samples
{
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IWebServiceArchiving
    {
        [OperationContract]
        void DoWork();
    }
}
