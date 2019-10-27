namespace e_CarSharing.Migrations
{
    using e_CarSharing.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<e_CarSharing.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "e_CarSharing.Models.ApplicationDbContext";
        }

        protected override void Seed(e_CarSharing.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            string[] roleNames = { "Admin", "User", "Owner" };
            foreach(var roles in roleNames)
            {
                if (!roleManager.RoleExists(roles))
                {
                    var role = new IdentityRole();
                    role.Name = roles;
                    roleManager.Create(role);
                }
            }

            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('BankAccounts', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Owners', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('RegularUsers', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Requests', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Vehicles', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('VehicleStations', RESEED, 0)");

            context.BankEntity.AddOrUpdate(b => b.BankAccountId, new Models.BankAccount()
            {
                BankAccountId = 1,
                BankName = "Caixa Geral de Depósitos",
                BankAccountNumber = "PT 5000 2145 8970"
            }); ;
            context.BankEntity.AddOrUpdate(b => b.BankAccountId, new Models.BankAccount()
            {
                BankAccountId = 2,
                BankName = "Santander",
                BankAccountNumber = "PT 5000 6783 1238"
            });

            context.RegularUser.AddOrUpdate(r => r.RegularUserId, new Models.RegularUser()
            {
                RegularUserId = 1,
                Name = "João Pedro",
                City = "Figueira da Foz",
                BankAccountId = 1
            });

            context.Owner.AddOrUpdate(o => o.OwnerId, new Models.Owner()
            {
                OwnerId = 1,
                Name = "Tiago Pinto",
                City = "Coimbra",
                OwnerType = Models.OwnerType.PARTICULAR,
                Address = "Rua do Zé Trolha",
                BankAccountId = 2
            });
        }
    }
}
