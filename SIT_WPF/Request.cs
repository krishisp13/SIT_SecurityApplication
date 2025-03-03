using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SIT_WPF
{
    public class Request
    {
        public int RequestID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class RequestContext : DbContext
    {
        public RequestContext() : base("name=RequestDB")
        {
        }

        public DbSet<Request> Requests { get; set; }
    }
}
