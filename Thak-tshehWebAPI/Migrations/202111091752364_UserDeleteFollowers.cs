namespace Thak_tshehWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDeleteFollowers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "ShowFollowing", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Users", "ShowFollowers", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Followers");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Followers", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "ShowFollowers", c => c.Boolean());
            AlterColumn("dbo.Users", "ShowFollowing", c => c.Boolean());
        }
    }
}
