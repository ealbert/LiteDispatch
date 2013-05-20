namespace LiteDispatch.Web.Filters
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Infrastructure;
  using System.Linq;
  using System.Threading;
  using System.Web.Mvc;
  using AutoMapper;
  using DbContext;
  using Domain.Entities;
  using Domain.Mappings;
  using Domain.Models;
  using Domain.Services;
  using EF.DbContext;
  using EF.TransManager;
  using Models;
  using WebMatrix.WebData;

  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
  public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
  {
    private static SimpleMembershipInitializer _initializer;
    private static object _initializerLock = new object();
    private static bool _isInitialized;

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      // Ensure ASP.NET Simple Membership is initialized only once per app start
      LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
    }

    private class SimpleMembershipInitializer
    {
      public SimpleMembershipInitializer()
      {
        Database.SetInitializer<UsersContext>(null);

        try
        {
          using (var context = new UsersContext())
          {
            if (!context.Database.Exists())
            {
              // Create the SimpleMembership database without Entity Framework migration schema
              ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
            }
          }

          WebSecurity.InitializeDatabaseConnection("SecurityDb", "UserProfile", "UserId", "UserName", autoCreateTables: true);
        }
        catch (Exception ex)
        {
          throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
        }

        using (var context = new LiteDispatchDbContext(new ModelCreator()))
        {
          if (context.Database.Exists()) return;
          context.Database.CreateIfNotExists();
          using (var transManager = Container.GlobalContext.TransFactory.CreateManager())
          {
            transManager.ExecuteCommand(locator =>
              {
                var haulier = locator.FindAll<Haulier>().SingleOrDefault(h => h.Name == "BlueWhale");
                if (haulier != null)
                {
                  return Mapper.Map<Haulier, HaulierModel>(haulier);
                }
                haulier = Haulier.Create(locator, new HaulierModel { Name = "BlueWhale" });
                return Mapper.Map<Haulier, HaulierModel>(haulier);
              });
          }
        }
      }
    }
  }
}
