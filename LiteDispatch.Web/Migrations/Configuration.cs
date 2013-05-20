namespace LiteDispatch.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LiteDispatch.Web.DbContext.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LiteDispatch.Web.DbContext.UsersContext context)
        {
            new SecurityDbManager().InitialiseDatabase();
        }
    }
}
