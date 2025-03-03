using System;

namespace RequestWebApp.Models
{
	public class Request
	{
        public int RequestID { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; } 
        public DateTime? UpdatedAt { get; set; }
    }
}
