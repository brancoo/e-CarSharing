namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVirtualToDeliveryAndRental1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "RentalExpectedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Rentals", "RentalDeliveryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rentals", "RentalDeliveryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Rentals", "RentalExpectedDate");
        }
    }
}
