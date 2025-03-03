using RequestWebApp.Models;
using System;
using System.Data.Entity;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() : base("name=ConnectionString") { }

    public DbSet<Request> Requests { get; set; }
}
