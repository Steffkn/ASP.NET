namespace Diploma.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Web.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = "Diploma.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(Diploma.Web.Models.ApplicationDbContext context)
        {
        }
    }
}
