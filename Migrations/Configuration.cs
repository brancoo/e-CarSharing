namespace e_CarSharing.Migrations
{
    using e_CarSharing.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<e_CarSharing.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(e_CarSharing.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            string[] roleNames = { "Admin", "Owner", "User" };

            foreach (var roles in roleNames)
            {
                if (!roleManager.RoleExists(roles))
                {
                    var role = new IdentityRole();
                    role.Name = roles;
                    roleManager.Create(role);
                }
            }

            IList<VehicleStation> vehicleStations = new List<VehicleStation>();

            vehicleStations.Add(new VehicleStation()
            {
                VehicleStationId = 1,
                Name = "Estação A",
                City = "Coimbra",
                Latitude = -36.49517,
                Longetide = -137.20044
            });
            vehicleStations.Add(new VehicleStation()
            {
                VehicleStationId = 2,
                Name = "Estação B",
                City = "Coimbra",
                Latitude = -27.40517,
                Longetide = -101.28984
            });
            foreach (VehicleStation vehicleStation in vehicleStations)
                context.VehicleStations.Add(vehicleStation);

            base.Seed(context);
        }
    }
}
