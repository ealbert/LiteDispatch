using System;
using System.Linq;
using System.Web;
using AutoMapper;
using LiteDispatch.Domain.Entities;
using LiteDispatch.Domain.Models;
using LiteDispatch.Domain.Repository;
using LiteDispatch.Domain.Services;
using LiteDispatch.Web.Models;

namespace LiteDispatch.Web.Services
{
  using DbContext;

  public static class LiteDispatchSession
  {

    public static HaulierModel UserHaulier()
    {
      return (HaulierModel)(HttpContext.Current.Session["UserHaulier"] ?? GetUserHaulier());
    }

    public static UserProfile UserProfile()
    {
      return (UserProfile)(HttpContext.Current.Session["UserProfile"] ?? GetUserProfile());
    }

    public static DispatchNoteModel LastDispatch
    {
      get { return HttpContext.Current.Session["LastDispatch"] as DispatchNoteModel; }
      set { HttpContext.Current.Session["LastDispatch"] = value; }
    }

    #region Private Methods
    
    private static HaulierModel GetUserHaulier()
    {      
      var haulier = ExecuteCommand(locator => GetHaulierByName(locator, GetUserProfile().HaulierName));
      HttpContext.Current.Session.Add("UserHaulier", haulier);
      return haulier;
    }

    private static HaulierModel GetHaulierByName(IRepositoryLocator locator, string haulierName)
    {
      var instance = locator.FindAll<Haulier>().Single(h => h.Name == haulierName);
      return Mapper.Map<Haulier, HaulierModel>(instance);
    }

    

    private static UserProfile GetUserProfile()
    {
      var context = new UsersContext();
      var userName = WebMatrix.WebData.WebSecurity.CurrentUserName.ToLower();
      var user = context.UserProfiles.SingleOrDefault(u => u.UserName.ToLower() == userName);
      HttpContext.Current.Session.Add("UserProfile", user);
      return user;
    }

    private static TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command) where TResult : class
    {
      using (var transManager = Container.GlobalContext.TransFactory.CreateManager())
      {
        return transManager.ExecuteCommand(command);
      }
    }

    #endregion
  }
}
