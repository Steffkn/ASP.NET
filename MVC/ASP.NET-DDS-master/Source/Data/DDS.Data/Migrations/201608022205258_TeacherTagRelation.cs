namespace DDS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class TeacherTagRelation : DbMigration
    {
        public override void Up()
        {
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
            this.DropForeignKey("dbo.TeacherTags", "Tag_Id", "dbo.Tags");
            this.DropForeignKey("dbo.TeacherTags", "Teacher_Id", "dbo.Teachers");
            this.DropIndex("dbo.TeacherTags", new[] { "Tag_Id" });
            this.DropIndex("dbo.TeacherTags", new[] { "Teacher_Id" });
            this.DropTable("dbo.TeacherTags");
        }
    }
}
