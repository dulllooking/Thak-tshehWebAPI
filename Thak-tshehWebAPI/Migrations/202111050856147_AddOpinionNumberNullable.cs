namespace Thak_tshehWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOpinionNumberNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "OpinionNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activities", "OpinionNumber", c => c.Int());
        }
    }
}
