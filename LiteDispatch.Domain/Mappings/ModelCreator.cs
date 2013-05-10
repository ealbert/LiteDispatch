namespace LiteDispatch.Domain.Mappings
{
  using System.Data.Entity;
  using System.Data.Entity.ModelConfiguration.Conventions;
  using Entities;
  using TransManager;

  /// <summary>
  /// LiteDispatch customised entity model creator 
  /// </summary>
  public class ModelCreator : IModelCreator
  {
    #region Implementation of IModelCreator

    public void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      modelBuilder.Configurations
                  .Add(new Haulier.Mapping())
                  .Add(new DispatchNote.Mapping());
    }

    #endregion
  }
}