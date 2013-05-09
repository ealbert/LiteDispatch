namespace LiteDispatch.EF.TransManager
{
  using System.Data.Entity;
  using Domain.TransManager;

  public class LiteDispatchDbContext : DbContext
  {
    public IModelCreator ModelCreator { get; private set; }

    public LiteDispatchDbContext(IModelCreator modelCreator)
      : base("DomainDb")
    {
      ModelCreator = modelCreator;
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      ModelCreator.OnModelCreating(modelBuilder);
    }
  }
}