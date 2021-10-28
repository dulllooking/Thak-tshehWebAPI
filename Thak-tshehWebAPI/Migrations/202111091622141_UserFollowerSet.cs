namespace Thak_tshehWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFollowerSet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserFollowers", "ActivityId", "dbo.Activities");
            DropIndex("dbo.UserFollowers", new[] { "ActivityId" });
            AddColumn("dbo.UserFollowers", "FollowingUserId", c => c.Int(nullable: false));
            DropColumn("dbo.UserFollowers", "ActivityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserFollowers", "ActivityId", c => c.Int(nullable: false));
            DropColumn("dbo.UserFollowers", "FollowingUserId");
            CreateIndex("dbo.UserFollowers", "ActivityId");
            AddForeignKey("dbo.UserFollowers", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
        }
    }
}
