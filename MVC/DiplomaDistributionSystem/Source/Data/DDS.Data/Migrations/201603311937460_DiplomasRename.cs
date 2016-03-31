namespace DDS.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DiplomasRename : DbMigration
    {
        public override void Up()
        {
            this.RenameTable("Diplomata", "Diplomas");
        }

        public override void Down()
        {
            this.RenameTable("Diplomas", "Diplomata");
        }
    }
}
