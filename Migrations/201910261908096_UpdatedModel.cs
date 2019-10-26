namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "BeingUsed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "BeingUsed");
        }
    }
}
