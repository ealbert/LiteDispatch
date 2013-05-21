namespace LiteDispatch.DbContext
{
  using System.Data.Entity.Infrastructure;
  using Domain.Mappings;

  public class DomainContextFactory
    : IDbContextFactory<LiteDispatchMigrationsDbContext>
  {
    public LiteDispatchMigrationsDbContext Create()
    {
      return new LiteDispatchMigrationsDbContext(new ModelCreator());
    }
  }
}