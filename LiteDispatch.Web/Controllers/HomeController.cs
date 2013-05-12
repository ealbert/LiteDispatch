using LiteDispatch.Web.Services;

namespace LiteDispatch.Web.Controllers
{
  using System.Web.Mvc;

  public class HomeController : Controller
  {
    [Authorize]
    public ActionResult Index()
    {
      ViewBag.Message = User.Identity.IsAuthenticated 
        ? LiteDispatchSession.UserProfile().HaulierName 
        : "Register before using the application";

      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your app description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}
