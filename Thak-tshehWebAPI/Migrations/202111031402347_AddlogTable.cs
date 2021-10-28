namespace Thak_tshehWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddlogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityCollects",
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
            
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        ActivityPrice = c.Int(),
                        OrderState = c.Boolean(),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 100),
                        UserMobilePhone = c.String(maxLength: 20),
                        CreatDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ActivityId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ActivityOpinions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        Star = c.Int(nullable: false),
                        Opinion = c.String(maxLength: 250),
                        CreatDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.OrganizerLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.TokenLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RefreshToken = c.String(maxLength: 50),
                        CreatDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TokenLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrganizerLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrganizerLogs", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.ActivityOpinions", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActivityOpinions", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.ActivityLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActivityLogs", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.ActivityCollects", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActivityCollects", "ActivityId", "dbo.Activities");
            DropIndex("dbo.TokenLogs", new[] { "UserId" });
            DropIndex("dbo.OrganizerLogs", new[] { "ActivityId" });
            DropIndex("dbo.OrganizerLogs", new[] { "UserId" });
            DropIndex("dbo.ActivityOpinions", new[] { "ActivityId" });
            DropIndex("dbo.ActivityOpinions", new[] { "UserId" });
            DropIndex("dbo.ActivityLogs", new[] { "UserId" });
            DropIndex("dbo.ActivityLogs", new[] { "ActivityId" });
            DropIndex("dbo.ActivityCollects", new[] { "ActivityId" });
            DropIndex("dbo.ActivityCollects", new[] { "UserId" });
            DropTable("dbo.TokenLogs");
            DropTable("dbo.OrganizerLogs");
            DropTable("dbo.ActivityOpinions");
            DropTable("dbo.ActivityLogs");
            DropTable("dbo.ActivityCollects");
        }
    }
}
