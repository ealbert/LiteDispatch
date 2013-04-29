namespace LiteDispatch.Web.Migrations
{
  using System.Web.Security;
  using Models;
  using WebMatrix.WebData;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<UsersContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = true;
    }

    protected override void Seed(UsersContext context)
    {
      WebSecurity.InitializeDatabaseConnection(
        "SecurityDb",
        "UserProfile",
        "UserId",
        "UserName", autoCreateTables: true);

      CreateAdminUser();
      CreateHaulierUser();
    }

    private void CreateAdminUser()
    {
      if (!Roles.RoleExists("Administrator")) Roles.CreateRole("Administrator");

      if (!WebSecurity.UserExists("admin"))
      {
        WebSecurity.CreateUserAndAccount(
          "admin",
          "password",
          new { EmailAddress = "admin.staff@lite.dispatch.com" });
      }

      if (!Roles.GetRolesForUser("admin").Contains("Administrator"))
      {
        Roles.AddUsersToRoles(new[] { "admin" }, new[] { "Administrator" });
      }
    }

    private void CreateHaulierUser()
    {
      if (!Roles.RoleExists("Haulier")) Roles.CreateRole("Haulier");

      if (!WebSecurity.UserExists("bluewhale"))
      {
        WebSecurity.CreateUserAndAccount(
          "bluewhale",
          "password",
          new { EmailAddress = "bluewhale.staff@lite.dispatch.com" });
      }

      if (!Roles.GetRolesForUser("bluewhale").Contains("Haulier"))
      {
        Roles.AddUsersToRoles(new[] { "bluewhale" }, new[] { "Haulier" });
      }
    }
  }
}
