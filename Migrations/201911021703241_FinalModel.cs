namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "RegularUserId", "dbo.RegularUsers");
            DropForeignKey("dbo.Requests", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Requests", "VehicleStationId", "dbo.VehicleStations");
            DropIndex("dbo.Requests", new[] { "RegularUserId" });
            DropIndex("dbo.Requests", new[] { "VehicleId" });
            DropIndex("dbo.Requests", new[] { "VehicleStationId" });
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        RentalId = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RentalId)
                .ForeignKey("dbo.Rentals", t => t.RentalId)
                .Index(t => t.RentalId);
            
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        RentalId = c.Int(nullable: false, identity: true),
                        RentalDate = c.DateTime(nullable: false),
                        RentalDeliveryDate = c.DateTime(nullable: false),
                        RegularUserId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        VehicleStationId = c.Int(nullable: false),
                        DeliveryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RentalId)
                .ForeignKey("dbo.RegularUsers", t => t.RegularUserId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: false)
                .ForeignKey("dbo.VehicleStations", t => t.VehicleStationId, cascadeDelete: true)
                .Index(t => t.RegularUserId)
                .Index(t => t.VehicleId)
                .Index(t => t.VehicleStationId);
            
            DropColumn("dbo.Owners", "Address");
            DropTable("dbo.Requests");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.RequestId);
            
            AddColumn("dbo.Owners", "Address", c => c.String(nullable: false, maxLength: 120));
            DropForeignKey("dbo.Rentals", "VehicleStationId", "dbo.VehicleStations");
            DropForeignKey("dbo.Rentals", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.Rentals", "RegularUserId", "dbo.RegularUsers");
            DropForeignKey("dbo.Deliveries", "RentalId", "dbo.Rentals");
            DropIndex("dbo.Rentals", new[] { "VehicleStationId" });
            DropIndex("dbo.Rentals", new[] { "VehicleId" });
            DropIndex("dbo.Rentals", new[] { "RegularUserId" });
            DropIndex("dbo.Deliveries", new[] { "RentalId" });
            DropTable("dbo.Rentals");
            DropTable("dbo.Deliveries");
            CreateIndex("dbo.Requests", "VehicleStationId");
            CreateIndex("dbo.Requests", "VehicleId");
            CreateIndex("dbo.Requests", "RegularUserId");
            AddForeignKey("dbo.Requests", "VehicleStationId", "dbo.VehicleStations", "VehicleStationId", cascadeDelete: true);
            AddForeignKey("dbo.Requests", "VehicleId", "dbo.Vehicles", "VehicleId", cascadeDelete: true);
            AddForeignKey("dbo.Requests", "RegularUserId", "dbo.RegularUsers", "RegularUserId", cascadeDelete: true);
        }
    }
}
