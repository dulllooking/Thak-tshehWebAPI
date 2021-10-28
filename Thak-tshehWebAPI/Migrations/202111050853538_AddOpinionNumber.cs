namespace Thak_tshehWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOpinionNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "OpinionNumber", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "OpinionNumber");
        }
    }
}
