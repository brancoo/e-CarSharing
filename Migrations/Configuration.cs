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

            IList<BankAccount> bankAccounts = new List<BankAccount>();
            IList<Owner> owners = new List<Owner>();
            IList<RegularUser> regularUsers = new List<RegularUser>();
            IList<Vehicle> vehicles = new List<Vehicle>();
            IList<VehicleStation> vehicleStations = new List<VehicleStation>();

            bankAccounts.Add(new BankAccount()
            {
                BankAccountId = 1,
                BankAccountNumber = "PT 5000 1234",
                BankName = "Caixa Geral de Depósitos"
            });
            bankAccounts.Add(new BankAccount()
            {
                BankAccountId = 2,
                BankAccountNumber = "PT 5000 4567",
                BankName = "Santander"
            });
            bankAccounts.Add(new BankAccount()
            {
                BankAccountId = 3,
                BankAccountNumber = "PT 5000 6912",
                BankName = "Novo Banco"
            });
            bankAccounts.Add(new BankAccount()
            {
                BankAccountId = 4,
                BankAccountNumber = "PT 5000 7120",
                BankName = "Novo Banco"
            });
            foreach (BankAccount bankAccount in bankAccounts)
                context.BankEntity.Add(bankAccount);


            owners.Add(new Owner()
            {
                OwnerId = 1,
                OwnerType = OwnerType.PARTICULAR,
                Name = "Joao Branco",
                City = "Coimbra",
                BankAccountId = 1
            });
            owners.Add(new Owner()
            {
                OwnerId = 2,
                OwnerType = OwnerType.PARTICULAR,
                Name = "Pedro Martins",
                City = "Coimbra",
                BankAccountId = 2
            });
            foreach (Owner owner in owners)
                context.Owner.Add(owner);

            regularUsers.Add(new RegularUser()
            {
                RegularUserId = 1,
                Name = "Alexandre Pinho",
                City = "Coimbra",
                BankAccountId = 3
            });
            regularUsers.Add(new RegularUser()
            {
                RegularUserId = 2,
                Name = "Maria José",
                City = "Condeixa",
                BankAccountId = 4
            });
            foreach (RegularUser regularUser in regularUsers)
                context.RegularUser.Add(regularUser);

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

            vehicles.Add(new Vehicle()
            {
                VehicleId = 1,
                VehicleType = VehicleType.CAR,
                Name = "Mercedes",
                OwnerId = 1,
                VehicleStationId = 1
            }); 
            vehicles.Add(new Vehicle()
            {
                VehicleId = 2,
                VehicleType = VehicleType.CAR,
                Name = "Porsche",
                OwnerId = 2,
                VehicleStationId = 2
            });
            foreach (Vehicle vehicle in vehicles)
                context.Vehicles.Add(vehicle);

            base.Seed(context);
        }
    }
}
