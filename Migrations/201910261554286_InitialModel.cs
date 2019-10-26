namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "VehicleStationId", "dbo.VehicleStations");
            DropForeignKey("dbo.Requests", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Requests", "RegularUserId", "dbo.RegularUsers");
            DropForeignKey("dbo.RegularUsers", "BankAccountId", "dbo.BankAccounts");
            DropForeignKey("dbo.Vehicles", "VehicleStationId", "dbo.VehicleStations");
            DropForeignKey("dbo.Vehicles", "OwnerId", "dbo.Owners");
            DropForeignKey("dbo.Owners", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Requests", new[] { "VehicleStationId" });
            DropIndex("dbo.Requests", new[] { "VehicleId" });
            DropIndex("dbo.Requests", new[] { "RegularUserId" });
            DropIndex("dbo.RegularUsers", new[] { "BankAccountId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleStationId" });
            DropIndex("dbo.Vehicles", new[] { "OwnerId" });
            DropIndex("dbo.Owners", new[] { "BankAccountId" });
            DropTable("dbo.Requests");
            DropTable("dbo.RegularUsers");
            DropTable("dbo.VehicleStations");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Owners");
            DropTable("dbo.BankAccounts");
        }
    }
}
