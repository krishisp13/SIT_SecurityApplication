using System.Collections.Generic;
using System.ServiceModel;

namespace SIT_WPF
{
    [ServiceContract]
    public interface IRequestService
    {
        [OperationContract]
        List<Request> CheckForRequests();

        [OperationContract]
        void UpdateRequest(int requestID, bool approve);
    }

}
