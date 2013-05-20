namespace LiteDispatch.Web.DbContext
{
  using System.Data.Entity;
  using Models;

  public class UsersContext : DbContext
  {
    public UsersContext()
      : base("SecurityDb")
    {
    }

    public DbSet<UserProfile> UserProfiles { get; set; }
  }
}