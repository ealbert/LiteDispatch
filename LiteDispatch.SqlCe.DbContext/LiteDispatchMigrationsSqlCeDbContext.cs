namespace LiteDispatch.SqlCe.DbContext
{
  using Domain.TransManager;
  using EF.DbContext;

  /// <summary>
  /// Replica of the LiteDispatchDbContext for
  /// the purpose of creating migrations
  /// </summary>
  public class LiteDispatchMigrationsSqlCeDbContext : LiteDispatchDbContext
  {
    public LiteDispatchMigrationsSqlCeDbContext(IModelCreator modelCreator) : base(modelCreator)
    {
    }
  }
}
