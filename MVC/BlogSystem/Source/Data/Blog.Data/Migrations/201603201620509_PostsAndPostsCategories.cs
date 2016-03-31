namespace Blog.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PostsAndPostsCategories : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CategoryId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PostCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.IsDeleted);

            this.CreateTable(
                "dbo.PostCategories",
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
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.Posts", "CategoryId", "dbo.PostCategories");
            this.DropIndex("dbo.PostCategories", new[] { "IsDeleted" });
            this.DropIndex("dbo.Posts", new[] { "IsDeleted" });
            this.DropIndex("dbo.Posts", new[] { "CategoryId" });
            this.DropTable("dbo.PostCategories");
            this.DropTable("dbo.Posts");
        }
    }
}
