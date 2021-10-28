namespace Thak_tshehWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbSet : DbMigration
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
                        EvaluateStars = c.Int(nullable: false),
                        CreatDate = c.DateTime(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
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
                        BirthDate = c.DateTime(),
                        Sex = c.Int(),
                        MobilePhone = c.String(maxLength: 50),
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
                        Followers = c.Int(nullable: false),
                        AccountState = c.Boolean(nullable: false),
                        CreatDate = c.DateTime(),
                        CheckMailCode = c.String(maxLength: 50),
                        MailCodeCreatDate = c.DateTime(nullable: false),
                        RefreshToken = c.String(maxLength: 50),
                        RefreshTokenCreatDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "User_Id", "dbo.Users");
            DropIndex("dbo.Activities", new[] { "User_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.CompanyInfoes");
            DropTable("dbo.Activities");
        }
    }
}
