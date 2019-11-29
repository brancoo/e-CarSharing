namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deliveries", "RentalId", "dbo.Rentals");
            DropIndex("dbo.Deliveries", new[] { "RentalId" });
            DropPrimaryKey("dbo.Deliveries");
            AddColumn("dbo.Deliveries", "DeliveryId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Rentals", "Delivery", c => c.Int());
            AddPrimaryKey("dbo.Deliveries", "DeliveryId");
            CreateIndex("dbo.Rentals", "Delivery");
            AddForeignKey("dbo.Rentals", "Delivery", "dbo.Deliveries", "DeliveryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "Delivery", "dbo.Deliveries");
            DropIndex("dbo.Rentals", new[] { "Delivery" });
            DropPrimaryKey("dbo.Deliveries");
            DropColumn("dbo.Rentals", "Delivery");
            DropColumn("dbo.Deliveries", "DeliveryId");
            AddPrimaryKey("dbo.Deliveries", "RentalId");
            CreateIndex("dbo.Deliveries", "RentalId");
            AddForeignKey("dbo.Deliveries", "RentalId", "dbo.Rentals", "RentalId");
        }
    }
}
