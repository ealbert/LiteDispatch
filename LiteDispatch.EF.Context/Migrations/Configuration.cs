namespace LiteDispatch.DbContext.Migrations
{
  using System.Data.Entity.Migrations;

  internal sealed class Configuration : DbMigrationsConfiguration<LiteDispatchMigrationsDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(LiteDispatchMigrationsDbContext context)
    {
      new DomainDbManager(context).Install();
    }
  }
}
