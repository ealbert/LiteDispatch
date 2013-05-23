using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteDispatch.Web.Controllers
{
  using BusinessAdapters;
  using Models;

  public class PascalController : Controller
  {
    public PascalController()
    {
      PascalAdapter = new PascalAdapter();
    }

    public PascalAdapter PascalAdapter { get; set; }

    public ActionResult Index()
    {
      var request = new PascalRequestModel();
      return View(request);
    }

    public ActionResult CalculateLevel(PascalRequestModel request)
    {
      if (ModelState.IsValid)
      {
        var model = PascalAdapter.CalculateLevel(request.Level);
        return View("Index", model);
      }

      // If we got this far, something failed, redisplay form
      ModelState.AddModelError("", "Please enter a valid level to calculate");
      return View("Index", request);
    }

    public ActionResult RefreshCache(int level)
    {

      PascalAdapter.FlushCache();
      ViewBag.Message = "Cache was flushed in the SQL Serve side";
      return View("Index", new PascalRequestModel{Level = level});
    }
  }
}
