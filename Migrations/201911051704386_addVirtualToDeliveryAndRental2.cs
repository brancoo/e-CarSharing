namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVirtualToDeliveryAndRental2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "DeliveryExpectedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Rentals", "RentalExpectedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rentals", "RentalExpectedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Rentals", "DeliveryExpectedDate");
        }
    }
}
