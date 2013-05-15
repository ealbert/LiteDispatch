namespace LiteDispatch.Web.App_Start
{
  using System.Web.Routing;

  /// <summary>
  /// SignalR Hubs will not work without a Hub route being configured. 
  /// To register the default Hubs route, call RouteConfig.RegisterRoutes(routes) in  
  /// your application's Application_Start method
  /// </summary>
  public class HubConfig
  {
    public static void RegisterHubs(RouteCollection routes)
    {
      routes.MapHubs();
    }
  }
}