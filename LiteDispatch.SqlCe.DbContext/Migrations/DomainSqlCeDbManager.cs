namespace LiteDispatch.SqlCe.DbContext.Migrations
{
  using LiteDispatch.DbContext.Migrations;
  using EF.DbContext;

  public class DomainSqlCeDbManager : DomainDbManager
  {
    public DomainSqlCeDbManager(LiteDispatchDbContext context) : base(context)
    {
    }

  }
}
