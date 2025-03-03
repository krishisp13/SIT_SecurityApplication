using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace SIT_WPF
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RequestService : IRequestService
    {
        public List<Request> CheckForRequests()
        {
            using (var db = new RequestContext())
            {
                return db.Requests.Where(r => r.Status == 0).ToList();
            }
        }

        public void UpdateRequest(int requestID, bool approve)
        {
            using (var db = new RequestContext())
            {
                var request = db.Requests.Find(requestID);
                if (request != null)
                {
                    request.Status = approve ? 1 : 2;
                    request.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }
    }


}
