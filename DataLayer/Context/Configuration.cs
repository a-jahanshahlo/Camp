using System.Data.Entity.Migrations;

namespace Camps.DataLayer.Context
{
    public class Configuration : DbMigrationsConfiguration<MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MainContext context)
        {
            base.Seed(context);
        }
    }
}