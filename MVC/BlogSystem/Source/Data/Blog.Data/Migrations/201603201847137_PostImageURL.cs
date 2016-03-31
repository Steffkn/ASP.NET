namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PostImageURL : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.Posts", "ImageURL", c => c.String());
        }

        public override void Down()
        {
            this.DropColumn("dbo.Posts", "ImageURL");
        }
    }
}
