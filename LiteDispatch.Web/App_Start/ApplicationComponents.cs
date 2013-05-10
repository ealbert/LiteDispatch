using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteDispatch.Web.App_Start
{
  using Domain.Mappings;
  using Domain.Services;
  using EF.TransManager;

  public class ApplicationComponents
  {
    public static void Register()
    {
      Container.GlobalContext.TransFactory = new TransManagerFactoryEF(new ModelCreator());
      ModelToEntity.Install();
    }
  }
}