namespace LiteDispatch.Domain.Repository
{
  using System.Linq;

  /// <summary>
  /// The repository locator is a place holder for finding all
  /// the individual repositories available within the application
  /// </summary>
  public interface IRepositoryLocator
  {
    #region CRUD operations

    TEntity Save<TEntity>(TEntity instance) where TEntity : class;
    void Update<TEntity>(TEntity instance) where TEntity : class;
    void Remove<TEntity>(TEntity instance) where TEntity : class;

    #endregion

    #region Retrieval Operations

    TEntity GetById<TEntity>(long id) where TEntity : class;
    IQueryable<TEntity> FindAll<TEntity>() where TEntity : class;

    #endregion

    /// <summary>
    /// Helper method that returns the repository for a given
    /// entity type
    /// </summary>
    /// <typeparam name="T">Entity type that the repository deals with</typeparam>
    /// <returns>Repository instance</returns>
    IRepository<T> GetRepository<T>() where T : class;

    /// <summary>
    /// Use to persist all changes so far done into the database
    /// </summary>
    void FlushModifications();

  }
}
