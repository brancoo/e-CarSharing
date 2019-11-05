namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rentals", "RegularUserId", "dbo.RegularUsers");
            DropForeignKey("dbo.Vehicles", "OwnerId", "dbo.Owners");
            DropIndex("dbo.Rentals", new[] { "RegularUserId" });
            DropIndex("dbo.Vehicles", new[] { "OwnerId" });
            AddColumn("dbo.Rentals", "RegularUser_RegularUserId", c => c.Int());
            AddColumn("dbo.Vehicles", "Owner_OwnerId", c => c.Int());
            AlterColumn("dbo.Rentals", "RegularUserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Vehicles", "OwnerId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Rentals", "RegularUserId");
            CreateIndex("dbo.Rentals", "RegularUser_RegularUserId");
            CreateIndex("dbo.Vehicles", "OwnerId");
            CreateIndex("dbo.Vehicles", "Owner_OwnerId");
            AddForeignKey("dbo.Rentals", "RegularUser_RegularUserId", "dbo.RegularUsers", "RegularUserId");
            AddForeignKey("dbo.Vehicles", "OwnerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Vehicles", "Owner_OwnerId", "dbo.Owners", "OwnerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "Owner_OwnerId", "dbo.Owners");
            DropForeignKey("dbo.Vehicles", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rentals", "RegularUser_RegularUserId", "dbo.RegularUsers");
            DropIndex("dbo.Vehicles", new[] { "Owner_OwnerId" });
            DropIndex("dbo.Vehicles", new[] { "OwnerId" });
            DropIndex("dbo.Rentals", new[] { "RegularUser_RegularUserId" });
            DropIndex("dbo.Rentals", new[] { "RegularUserId" });
            AlterColumn("dbo.Vehicles", "OwnerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Rentals", "RegularUserId", c => c.Int(nullable: false));
            DropColumn("dbo.Vehicles", "Owner_OwnerId");
            DropColumn("dbo.Rentals", "RegularUser_RegularUserId");
            CreateIndex("dbo.Vehicles", "OwnerId");
            CreateIndex("dbo.Rentals", "RegularUserId");
            AddForeignKey("dbo.Vehicles", "OwnerId", "dbo.Owners", "OwnerId", cascadeDelete: true);
            AddForeignKey("dbo.Rentals", "RegularUserId", "dbo.RegularUsers", "RegularUserId", cascadeDelete: true);
        }
    }
}
