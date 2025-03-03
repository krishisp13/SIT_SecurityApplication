using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RequestWebApp.Models
{
	public class DbContextFactory
	{
        public ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}