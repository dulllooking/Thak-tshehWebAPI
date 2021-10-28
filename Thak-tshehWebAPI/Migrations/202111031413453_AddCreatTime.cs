namespace Thak_tshehWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatTime : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activities", "User_Id", "dbo.Users");
            DropIndex("dbo.Activities", new[] { "User_Id" });
            AddColumn("dbo.OrganizerLogs", "CreatDate", c => c.DateTime());
            AddColumn("dbo.CompanyInfoes", "CreatDate", c => c.DateTime());
            DropColumn("dbo.Activities", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Activities", "User_Id", c => c.Int());
            DropColumn("dbo.CompanyInfoes", "CreatDate");
            DropColumn("dbo.OrganizerLogs", "CreatDate");
            CreateIndex("dbo.Activities", "User_Id");
            AddForeignKey("dbo.Activities", "User_Id", "dbo.Users", "Id");
        }
    }
}
