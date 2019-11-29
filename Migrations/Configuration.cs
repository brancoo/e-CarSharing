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
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

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

            var user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
            var admin = userManager.Create(user, "admin");
            if (admin.Succeeded)
            {
                //here we tie the new user to the role
                userManager.AddToRole(user.Id, "Admin");
            }

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('BankAccounts', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Owners', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('RegularUsers', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Rentals', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Vehicles', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('VehicleStations', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Deliveries', RESEED, 0)");
        }
    }
}
