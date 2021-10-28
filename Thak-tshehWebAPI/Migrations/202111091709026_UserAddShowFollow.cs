namespace Thak_tshehWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAddShowFollow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ShowComingActivity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "ShowFollowing", c => c.Boolean());
            AddColumn("dbo.Users", "ShowFollowers", c => c.Boolean());
            DropColumn("dbo.Users", "ShowWeekActivity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ShowWeekActivity", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "ShowFollowers");
            DropColumn("dbo.Users", "ShowFollowing");
            DropColumn("dbo.Users", "ShowComingActivity");
        }
    }
}
