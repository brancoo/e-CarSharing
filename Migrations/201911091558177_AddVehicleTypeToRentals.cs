namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVehicleTypeToRentals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "VehicleType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rentals", "VehicleType");
        }
    }
}
