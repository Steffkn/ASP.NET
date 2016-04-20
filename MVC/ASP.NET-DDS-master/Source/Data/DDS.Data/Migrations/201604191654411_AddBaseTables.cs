namespace DDS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddBaseTables : DbMigration
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
                    ApprovedByLeader = c.Boolean(nullable: false),
                    ApprovedByHead = c.Boolean(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                    Leader_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.Leader_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.Leader_Id);

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
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.User_Id);

            this.CreateTable(
                "dbo.Students",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FNumber = c.String(),
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
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.SelectedDiploma_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Teacher_Id);

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

        }

        public override void Down()
        {
            this.DropForeignKey("dbo.TagDiplomas", "Diploma_Id", "dbo.Diplomas");
            this.DropForeignKey("dbo.TagDiplomas", "Tag_Id", "dbo.Tags");
            this.DropForeignKey("dbo.Diplomas", "Leader_Id", "dbo.Teachers");
            this.DropForeignKey("dbo.Teachers", "User_Id", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Students", "Teacher_Id", "dbo.Teachers");
            this.DropForeignKey("dbo.Students", "User_Id", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Students", "SelectedDiploma_Id", "dbo.Diplomas");
            this.DropIndex("dbo.TagDiplomas", new[] { "Diploma_Id" });
            this.DropIndex("dbo.TagDiplomas", new[] { "Tag_Id" });
            this.DropIndex("dbo.Tags", new[] { "IsDeleted" });
            this.DropIndex("dbo.Students", new[] { "Teacher_Id" });
            this.DropIndex("dbo.Students", new[] { "User_Id" });
            this.DropIndex("dbo.Students", new[] { "SelectedDiploma_Id" });
            this.DropIndex("dbo.Students", new[] { "IsDeleted" });
            this.DropIndex("dbo.Teachers", new[] { "User_Id" });
            this.DropIndex("dbo.Teachers", new[] { "IsDeleted" });
            this.DropIndex("dbo.Diplomas", new[] { "Leader_Id" });
            this.DropIndex("dbo.Diplomas", new[] { "IsDeleted" });
            this.DropTable("dbo.TagDiplomas");
            this.DropTable("dbo.Tags");
            this.DropTable("dbo.Students");
            this.DropTable("dbo.Teachers");
            this.DropTable("dbo.Diplomas");
        }
    }
}
