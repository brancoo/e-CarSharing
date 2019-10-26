using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace e_CarSharing.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleStation> VehicleStations { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RegularUser> RegularUser { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<BankAccount> BankEntity { get; set; }
    }
}