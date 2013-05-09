namespace LiteDispatch.EF.Repository
{
  using System.Data.Entity;
  using System.Data.Objects;
  using TransManager;
  using Domain.Repository;

  /// <summary>
  /// EF Repository Locator implementation
  /// </summary>
  public class RepositoryLocatorEF
    : RepositoryLocatorBase
  {
    private readonly DbContext _dbContext;

    public RepositoryLocatorEF(DbContext dbContext)
    {
      _dbContext = dbContext;
    }

    #region Overrides of RepositoryLocatorBase

    /// <summary>
    /// The EF overwrites this method so it is possible to get
    /// entities IDs before the transaction is committed which
    /// is handy if a DTO or Model is returned by the function
    /// invoked
    /// </summary>
    public override void FlushModifications()
    {
      base.FlushModifications();
      _dbContext.GetObjectContext().SaveChanges(SaveOptions.DetectChangesBeforeSave);
    }

    protected override IRepository<TEntity> CreateRepository<TEntity>()
    {
      return new RepositoryEF<TEntity>(_dbContext.Set<TEntity>());
    }

    #endregion
  }
}
