namespace LiteDispatch.EF.TransManager
{
  using System.Data.Entity;
  using System.Data.Entity.Infrastructure;
  using System.Data.Objects;

  public static class DbContextExtensions
  {
    /// <summary>
    /// Helper method to obtain the <see cref="ObjectContext"/> from
    /// the <see cref="DbContext"/>
    /// </summary>
    /// <param name="dbContext">DbContext that have a reference to the ObjectContext</param>
    /// <returns>ObjectContext instance</returns>
    public static ObjectContext GetObjectContext(this DbContext dbContext)
    {
      return ((IObjectContextAdapter) dbContext).ObjectContext;
    }
  }
}