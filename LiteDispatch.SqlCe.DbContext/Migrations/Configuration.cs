namespace LiteDispatch.SqlCe.DbContext.Migrations
{
  using System.Data.Entity.Migrations;

  internal sealed class Configuration :
    DbMigrationsConfiguration<LiteDispatch.SqlCe.DbContext.LiteDispatchMigrationsSqlCeDbContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(LiteDispatchMigrationsSqlCeDbContext context)
    {
      new DomainSqlCeDbManager(context).Install();
    }
  }
}
