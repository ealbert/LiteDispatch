namespace LiteDispatch.DbContext
{
  using Domain.TransManager;
  using EF.DbContext;

  /// <summary>
  /// Replica of the LiteDispatchDbContext for
  /// the purpose of creating migrations
  /// </summary>
  public class LiteDispatchMigrationsDbContext : LiteDispatchDbContext
  {
    public LiteDispatchMigrationsDbContext(IModelCreator modelCreator) : base(modelCreator)
    {
    }
  }
}
