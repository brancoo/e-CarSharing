namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<e_CarSharing.Models.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(e_CarSharing.Models.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
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
            }); ; ;

        }
    }
}
