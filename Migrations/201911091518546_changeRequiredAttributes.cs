namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeRequiredAttributes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rentals", "RegularUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Rentals", new[] { "RegularUserId" });
            AlterColumn("dbo.Rentals", "RegularUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Rentals", "RegularUserId");
            AddForeignKey("dbo.Rentals", "RegularUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "RegularUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Rentals", new[] { "RegularUserId" });
            AlterColumn("dbo.Rentals", "RegularUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Rentals", "RegularUserId");
            AddForeignKey("dbo.Rentals", "RegularUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
