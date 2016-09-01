namespace DDS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Diplomas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    ExperimentalPart = c.String(nullable: false),
                    ContentCSV = c.String(),
                    TeacherID = c.Int(nullable: false),
                    IsApprovedByLeader = c.Boolean(nullable: false),
                    IsApprovedByHead = c.Boolean(nullable: false),
                    IsSelectedByStudent = c.Boolean(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TeacherID)
                .Index(t => t.IsDeleted);

            this.CreateTable(
                "dbo.Tags",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);

            this.CreateTable(
                "dbo.Teachers",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                    User_Id = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.User_Id);

            this.CreateTable(
                "dbo.Students",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FNumber = c.Int(nullable: false),
                    Address = c.String(),
                    IsGraduate = c.Boolean(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                    SelectedDiploma_Id = c.Int(),
                    User_Id = c.String(nullable: false, maxLength: 128),
                    Teacher_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diplomas", t => t.SelectedDiploma_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.SelectedDiploma_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Teacher_Id);

            this.CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                    MiddleName = c.String(nullable: false),
                    ScienceDegree = c.String(),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            this.CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            this.CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            this.CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            this.CreateTable(
                "dbo.Messages",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SenderUserId = c.String(),
                    ResieverUserId = c.String(),
                    MessageSend = c.String(nullable: false),
                    IsRead = c.Boolean(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                    SelectedDiploma_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diplomas", t => t.SelectedDiploma_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.SelectedDiploma_Id);

            this.CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            this.CreateTable(
                "dbo.TagDiplomas",
                c => new
                {
                    Tag_Id = c.Int(nullable: false),
                    Diploma_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Tag_Id, t.Diploma_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Diplomas", t => t.Diploma_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Diploma_Id);

            this.CreateTable(
                "dbo.TeacherTags",
                c => new
                {
                    Teacher_Id = c.Int(nullable: false),
                    Tag_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Teacher_Id, t.Tag_Id })
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Tag_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Messages", "SelectedDiploma_Id", "dbo.Diplomas");
            DropForeignKey("dbo.Diplomas", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherTags", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TeacherTags", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Students", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Students", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Students", "SelectedDiploma_Id", "dbo.Diplomas");
            DropForeignKey("dbo.TagDiplomas", "Diploma_Id", "dbo.Diplomas");
            DropForeignKey("dbo.TagDiplomas", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TeacherTags", new[] { "Tag_Id" });
            DropIndex("dbo.TeacherTags", new[] { "Teacher_Id" });
            DropIndex("dbo.TagDiplomas", new[] { "Diploma_Id" });
            DropIndex("dbo.TagDiplomas", new[] { "Tag_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Messages", new[] { "SelectedDiploma_Id" });
            DropIndex("dbo.Messages", new[] { "IsDeleted" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Students", new[] { "Teacher_Id" });
            DropIndex("dbo.Students", new[] { "User_Id" });
            DropIndex("dbo.Students", new[] { "SelectedDiploma_Id" });
            DropIndex("dbo.Students", new[] { "IsDeleted" });
            DropIndex("dbo.Teachers", new[] { "User_Id" });
            DropIndex("dbo.Teachers", new[] { "IsDeleted" });
            DropIndex("dbo.Tags", new[] { "IsDeleted" });
            DropIndex("dbo.Diplomas", new[] { "IsDeleted" });
            DropIndex("dbo.Diplomas", new[] { "TeacherID" });
            DropTable("dbo.TeacherTags");
            DropTable("dbo.TagDiplomas");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Students");
            DropTable("dbo.Teachers");
            DropTable("dbo.Tags");
            DropTable("dbo.Diplomas");
        }
    }
}
