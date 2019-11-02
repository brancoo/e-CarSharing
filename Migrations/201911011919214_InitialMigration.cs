namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankAccountId = c.Int(nullable: false, identity: true),
                        BankName = c.String(nullable: false, maxLength: 100),
                        BankAccountNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BankAccountId);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        OwnerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        OwnerType = c.Int(nullable: false),
                        City = c.String(nullable: false, maxLength: 60),
                        Address = c.String(nullable: false, maxLength: 120),
                        BankAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OwnerId)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        VehicleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        VehicleType = c.Int(nullable: false),
                        OwnerId = c.Int(nullable: false),
                        VehicleStationId = c.Int(nullable: false),
                        BeingUsed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VehicleId)
                .ForeignKey("dbo.Owners", t => t.OwnerId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleStations", t => t.VehicleStationId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.VehicleStationId);
            
            CreateTable(
                "dbo.VehicleStations",
                c => new
                    {
                        VehicleStationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                        Latitude = c.Double(nullable: false),
                        Longetide = c.Double(nullable: false),
                        City = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.VehicleStationId);
            
            CreateTable(
                "dbo.RegularUsers",
                c => new
                    {
                        RegularUserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        City = c.String(nullable: false, maxLength: 60),
                        BankAccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RegularUserId)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId, cascadeDelete: true)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        RequestDate = c.DateTime(nullable: false),
                        ExpectedTime = c.Int(nullable: false),
                        RegularUserId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        VehicleStationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.RegularUsers", t => t.RegularUserId, cascadeDelete: false)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: false)
                .ForeignKey("dbo.VehicleStations", t => t.VehicleStationId, cascadeDelete: false)
                .Index(t => t.RegularUserId)
                .Index(t => t.VehicleId)
                .Index(t => t.VehicleStationId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Requests", "VehicleStationId", "dbo.VehicleStations");
            DropForeignKey("dbo.Requests", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Requests", "RegularUserId", "dbo.RegularUsers");
            DropForeignKey("dbo.RegularUsers", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.Vehicles", "VehicleStationId", "dbo.VehicleStations");
            DropForeignKey("dbo.Vehicles", "OwnerId", "dbo.Owners");
            DropForeignKey("dbo.Owners", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Requests", new[] { "VehicleStationId" });
            DropIndex("dbo.Requests", new[] { "VehicleId" });
            DropIndex("dbo.Requests", new[] { "RegularUserId" });
            DropIndex("dbo.RegularUsers", new[] { "BankAccountId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleStationId" });
            DropIndex("dbo.Vehicles", new[] { "OwnerId" });
            DropIndex("dbo.Owners", new[] { "BankAccountId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Requests");
            DropTable("dbo.RegularUsers");
            DropTable("dbo.VehicleStations");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Owners");
            DropTable("dbo.BankAccounts");
        }
    }
}
