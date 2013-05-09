namespace LiteDispatch.EF.Repository
{
  using System.Linq;
  using System.Data.Entity;
  using Domain.Repository;

  /// <summary>
  /// EF generic repository implementation
  /// </summary>
  /// <typeparam name="TEntity">Entity type that this repository supports</typeparam>
  public class RepositoryEF<TEntity>
    : IRepository<TEntity> where TEntity : class
  {
    private readonly IDbSet<TEntity> _dbSet;

    public RepositoryEF(IDbSet<TEntity> dbSet)
    {
      _dbSet = dbSet;
    }

    #region Implementation of IRepository<TEntity>

    public TEntity Save(TEntity instance)
    {
      return _dbSet.Add(instance);
    }

    public void Update(TEntity instance)
    {
    }

    public void Remove(TEntity instance)
    {
      _dbSet.Remove(instance);
    }

    public TEntity GetById(long id)
    {
      return _dbSet.Find(id);
    }

    public IQueryable<TEntity> FindAll()
    {
      return _dbSet.AsQueryable();
    }

    #endregion
  }
}
