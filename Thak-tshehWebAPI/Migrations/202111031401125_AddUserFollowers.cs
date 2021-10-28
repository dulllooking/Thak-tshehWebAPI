namespace Thak_tshehWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserFollowers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFollowers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        CreatDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFollowers", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserFollowers", "ActivityId", "dbo.Activities");
            DropIndex("dbo.UserFollowers", new[] { "ActivityId" });
            DropIndex("dbo.UserFollowers", new[] { "UserId" });
            DropTable("dbo.UserFollowers");
        }
    }
}
