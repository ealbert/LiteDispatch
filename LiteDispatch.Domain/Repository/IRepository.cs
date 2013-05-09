namespace LiteDispatch.Domain.Repository
{
  using System.Linq;

  /// <summary>
  /// Defines basic CRUD operations that a repository implementation
  /// must comply with
  /// </summary>
  /// <typeparam name="TEntity">This is a generic repository implementation</typeparam>
  public interface IRepository<TEntity>
  {
    #region CRUD operations

    TEntity Save(TEntity instance);
    void Update(TEntity instance);
    void Remove(TEntity instance);

    #endregion

    #region Retrieval Operations

    TEntity GetById(long id);
    IQueryable<TEntity> FindAll();

    #endregion
  }
}
