namespace LiteDispatch.SqlCe.DbContext
{
  using System.Data.Entity.Infrastructure;
  using Domain.Mappings;

  public class SqlCeDomainContextFactory
    : IDbContextFactory<LiteDispatchMigrationsSqlCeDbContext>
  {
    public LiteDispatchMigrationsSqlCeDbContext Create()
    {
      return new LiteDispatchMigrationsSqlCeDbContext(new ModelCreator());
    }
  }
}