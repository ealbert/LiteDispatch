namespace LiteDispatch.Domain.TransManager
{
  using System.Data.Entity;

  /// <summary>
  /// A model creator is used to initialise the xxx
  /// </summary>
  public interface IModelCreator
  {
    void OnModelCreating(DbModelBuilder modelBuilder);
  }
}