namespace ElementsWeb.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Characters",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    RandomNumber = c.Int(nullable: false),
                    MaxHealth = c.Int(nullable: false),
                    MaxResource = c.Int(nullable: false),
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

        }

        public override void Down()
        {
            this.DropForeignKey("dbo.Characters", "User_Id", "dbo.AspNetUsers");
            this.DropIndex("dbo.Characters", new[] { "User_Id" });
            this.DropIndex("dbo.Characters", new[] { "IsDeleted" });
            this.DropTable("dbo.Characters");
        }
    }
}
