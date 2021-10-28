namespace Thak_tshehWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Image = c.String(maxLength: 200),
                        ActivityClass = c.Int(nullable: false),
                        ActivityType = c.Int(nullable: false),
                        ActivityVenue = c.Int(nullable: false),
                        Software = c.Int(nullable: false),
                        Area = c.Int(nullable: false),
                        Address = c.String(maxLength: 50),
                        AddressRemark = c.String(maxLength: 50),
                        ActivityStartDate = c.DateTime(nullable: false),
                        ActivityEndDate = c.DateTime(nullable: false),
                        Summary = c.String(maxLength: 250),
                        ContentText = c.String(),
                        PleaseNote = c.String(maxLength: 250),
                        OrganizerName = c.String(maxLength: 50),
                        OrganizerPhone = c.String(maxLength: 50),
                        OrganizerMail = c.String(maxLength: 50),
                        Link = c.String(maxLength: 200),
                        Price = c.Int(nullable: false),
                        LimitNumber = c.Int(nullable: false),
                        StartAcceptDate = c.DateTime(nullable: false),
                        EndAcceptDate = c.DateTime(nullable: false),
                        PostState = c.Boolean(nullable: false),
                        FreeCost = c.Boolean(nullable: false),
                        ApplicantNumber = c.Int(nullable: false),
                        ApplicantFull = c.Boolean(nullable: false),
                        Views = c.Int(nullable: false),
                        CollectNumber = c.Int(nullable: false),
                        CreatDate = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActivityCollects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        CreatDate = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 50),
                        HashPassword = c.String(nullable: false, maxLength: 100),
                        Salt = c.String(maxLength: 50),
                        Image = c.String(maxLength: 200),
                        Name = c.String(maxLength: 50),
                        NickName = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        Area = c.String(maxLength: 50),
                        FacebookLink = c.String(maxLength: 200),
                        InstagramLink = c.String(maxLength: 200),
                        AboutMe = c.String(maxLength: 200),
                        MySkill = c.String(maxLength: 200),
                        MyInterest = c.String(maxLength: 200),
                        ShowWeekActivity = c.Boolean(nullable: false),
                        ShowNeedEvalActivity = c.Boolean(nullable: false),
                        ShowCollectActivity = c.Boolean(nullable: false),
                        ShowFinishActivity = c.Boolean(nullable: false),
                        ShowCancelActivity = c.Boolean(nullable: false),
                        Views = c.Int(nullable: false),
                        AccountState = c.Boolean(nullable: false),
                        CreatDate = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                        CheckMailCode = c.String(maxLength: 50),
                        MailCodeCreatDate = c.DateTime(),
                        RefreshToken = c.String(maxLength: 50),
                        RefreshTokenCreatDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActivityLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        OrderState = c.Boolean(nullable: false),
                        CreatDate = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.ActivityOpinions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        Star = c.Int(nullable: false),
                        Opinion = c.String(maxLength: 250),
                        CreatDate = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.MailCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CheckCode = c.String(maxLength: 50),
                        CreatDate = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TokenLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RefreshToken = c.String(maxLength: 50),
                        CreatDate = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserFollowers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        CreatDate = c.DateTime(nullable: false, defaultValueSql: "GetDate()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.CompanyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        FacebookLink = c.String(maxLength: 200),
                        InstagramLink = c.String(maxLength: 200),
                        LineLink = c.String(maxLength: 200),
                        AboutUs = c.String(),
                        Questions = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFollowers", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserFollowers", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.TokenLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.MailCodes", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActivityOpinions", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActivityOpinions", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.ActivityLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActivityLogs", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.ActivityCollects", "UserId", "dbo.Users");
            DropForeignKey("dbo.ActivityCollects", "ActivityId", "dbo.Activities");
            DropIndex("dbo.UserFollowers", new[] { "ActivityId" });
            DropIndex("dbo.UserFollowers", new[] { "UserId" });
            DropIndex("dbo.TokenLogs", new[] { "UserId" });
            DropIndex("dbo.MailCodes", new[] { "UserId" });
            DropIndex("dbo.ActivityOpinions", new[] { "ActivityId" });
            DropIndex("dbo.ActivityOpinions", new[] { "UserId" });
            DropIndex("dbo.ActivityLogs", new[] { "ActivityId" });
            DropIndex("dbo.ActivityLogs", new[] { "UserId" });
            DropIndex("dbo.ActivityCollects", new[] { "ActivityId" });
            DropIndex("dbo.ActivityCollects", new[] { "UserId" });
            DropTable("dbo.CompanyInfoes");
            DropTable("dbo.UserFollowers");
            DropTable("dbo.TokenLogs");
            DropTable("dbo.MailCodes");
            DropTable("dbo.ActivityOpinions");
            DropTable("dbo.ActivityLogs");
            DropTable("dbo.Users");
            DropTable("dbo.ActivityCollects");
            DropTable("dbo.Activities");
        }
    }
}
