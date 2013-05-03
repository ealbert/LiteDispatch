﻿namespace LiteDispatch.Web.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.Mvc;
  using Models;
  using WebMatrix.WebData;

  public class DispatchController : Controller
  {
    public ActionResult Index()
    {
      if (TempData.ContainsKey("NotificationMsg"))
      {
        ViewBag.NotificationMsg = TempData["NotificationMsg"];
      }
      return View();
    }

    public ActionResult UploadFile(UploadDispatchModel model, HttpPostedFileBase uploadedFile)
    {
      var invalidFlag = IsInvalidUploadFile(uploadedFile);
      if (!ModelState.IsValid || invalidFlag)
      {
        ModelState.AddModelError("", "Please, check that all fields were entered correctly");
        if (invalidFlag)
        {
          ModelState.AddModelError("", InvalidUploadFileNotification(uploadedFile));
        }
        return View("Index", model);
      }
      return RedirectToAction("ValidateDispatch", model);
    }

    public ActionResult DisplayDispatch(Guid listadoGuid)
    {
      var listado = Dispatches().SingleOrDefault(l => l.Guid == listadoGuid);
      return View(listado);
    }

    private bool IsInvalidUploadFile(HttpPostedFileBase uploadedFile)
    {
      if (uploadedFile == null) return true;
      if (uploadedFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") return true;
      return false;
    }

    private string InvalidUploadFileNotification(HttpPostedFileBase uploadedFile)
    {
      if (uploadedFile == null) return "Select an excel document with the dispatch information";
      if (uploadedFile.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
      {
        return "The uploaded file is not an XLSX Excel document";
      }
      return "The uploaded file is invalid";
    }

    public ActionResult ValidateDispatch(UploadDispatchModel model)
    {
      var dispatch = GetDispatchNote(model);
      const string msg = "{0} - New dispatch note was created with date {1:d} and contains {2} lines";
      ViewBag.Message = string.Format(msg, dispatch.Transportista, dispatch.DispatchDate, dispatch.Lines.Count);
      Session.Add("Dispatch", dispatch);
      return View(dispatch);
    }

    public ActionResult PrintDispatch(Guid listadoGuid)
    {
      var dispatch = Dispatches().SingleOrDefault(l => l.Guid == listadoGuid);
      return View(dispatch);
    }      

    public ActionResult Enquiry()
    {
      var model = Dispatches();
      return View(model);
    }

    public ActionResult ExcelTemplate()
    {
      return File("~/Content/documents/Dispatch_Template.xlsx", "application/vnd.ms-excel", "Dispatch_Template.xlsx");
    }

    private DispatchModel GetDispatchNote(UploadDispatchModel model)
    {
      var result = new DispatchModel {State = "Received", DispatchDate = model.DispatchDate.Value, Transportista = "KillerLogistics", TruckReg = model.TruckReg, DispatchReference = model.ReferenceNumber};
      var linea = new DispatchLineModel
        {
          LineId = 1,
          ProductType = "Fresh",
          Product = "Hake",
          Metric = "Kg",
          Quantity = 25,
          ShopId = 18,
          Client = "RedSquid"
        };

      result.Lines.Add(linea);

      linea = new DispatchLineModel
      {
        LineId = 2,
        ProductType = "Frozen",
        Product = "Frozen Squid",
        Metric = "Pallet",
        Quantity = 4,
        ShopId = 4,
        ShopLetter = "A",
        Client = "Alaska Brothers"
      };

      result.Lines.Add(linea);

      linea = new DispatchLineModel
      {
        LineId = 3,
        ProductType = "Shellfish",
        Product = "Mussel",
        Metric = "Sac",
        Quantity = 20,
        ShopId = 112,
        Client = "Irish Seafoods"
      };

      result.Lines.Add(linea);

      return result;
    }

    public ActionResult Confirm()
    {
      TempData["NotificationMsg"] = "Last dispatch note was confirmed";
      var dispatchModel = (DispatchModel) Session["Dispatch"];
      dispatchModel.CreationDate = DateTime.Now;
      dispatchModel.Guid = Guid.NewGuid();
      dispatchModel.User = WebSecurity.CurrentUserName;
      Dispatches().Add(dispatchModel);
      return RedirectToAction("Enquiry");
    }

    public ActionResult Discard()
    {
      TempData["NotificationMsg"] = "Last dispatch note was discarded";
      return RedirectToAction("Index");
    }

    private List<DispatchModel> Dispatches()
    {
      if (Session["Dispatches"] == null)
      {
        Session["Dispatches"] = new List<DispatchModel>();
      }
      return (List<DispatchModel>) Session["Dispatches"];
    }
  }
}