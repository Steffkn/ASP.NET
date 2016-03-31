namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostsAndPostsCategoriesAuthorID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "AuthorID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "AuthorID");
        }
    }
}
