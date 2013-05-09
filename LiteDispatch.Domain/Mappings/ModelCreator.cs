namespace LiteDispatch.Domain.Mappings
{
  using System.Data.Entity;
  using System.Data.Entity.ModelConfiguration.Conventions;
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
      // Add root aggregate entities here
      //modelBuilder.Configurations.Add(new Customer.Mapping());
      //modelBuilder.Entity<Address>();
    }

    #endregion
  }
}