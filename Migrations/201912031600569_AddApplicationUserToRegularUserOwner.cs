namespace e_CarSharing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationUserToRegularUserOwner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Owners", "UserId", c => c.String(maxLength: 128));
            AddColumn("dbo.RegularUsers", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Owners", "UserId");
            CreateIndex("dbo.RegularUsers", "UserId");
            AddForeignKey("dbo.Owners", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.RegularUsers", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegularUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Owners", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.RegularUsers", new[] { "UserId" });
            DropIndex("dbo.Owners", new[] { "UserId" });
            DropColumn("dbo.RegularUsers", "UserId");
            DropColumn("dbo.Owners", "UserId");
        }
    }
}
