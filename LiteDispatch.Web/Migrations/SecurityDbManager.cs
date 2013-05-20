using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteDispatch.Web.Migrations
{
  using System.Web.Security;
  using WebMatrix.WebData;

  public class SecurityDbManager
  {
    public void InitialiseDatabase()
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

      if (!WebSecurity.UserExists("codeproject"))
      {
        WebSecurity.CreateUserAndAccount(
          "codeproject",
          "litedispatch",
          new { EmailAddress = "bluewhale.staff@lite.dispatch.com", HaulierName = "BlueWhale" });
      }

      if (!Roles.GetRolesForUser("codeproject").Contains("Haulier"))
      {
        Roles.AddUsersToRoles(new[] { "codeproject" }, new[] { "Haulier" });
      }
    }
  }
}