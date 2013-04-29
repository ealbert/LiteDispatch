namespace LiteDispatch.Web.Controllers
{
  using System.Web.Mvc;

  public class HomeController : Controller
  {
    [Authorize]
    public ActionResult Index()
    {
      if (User.Identity.IsAuthenticated)
      {
        ViewBag.Message = "[Haulier's name]";  
      }
      else
      {
        ViewBag.Message = "Register before using the application";
      }
      

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
