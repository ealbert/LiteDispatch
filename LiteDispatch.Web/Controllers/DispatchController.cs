namespace LiteDispatch.Web.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.Mvc;
  using Models;

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

    public ActionResult UploadFile(UploadListadoModel model, HttpPostedFileBase uploadedFile)
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
      return RedirectToAction("ValidateListado", model);
    }

    public ActionResult DisplayListado(Guid listadoGuid)
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

    public ActionResult ValidateListado(UploadListadoModel model)
    {
      var listado = GetAlbaran(model);
      const string msg = "{0} - New dispatch note was created with date {1:d} and contains {2} lines";
      ViewBag.Message = string.Format(msg, listado.Transportista, listado.Fecha, listado.Lineas.Count);
      Session.Add("Dispatch", listado);
      return View(listado);
    }

    public ActionResult ImprimirListado(Guid listadoGuid)
    {
      var listado = Dispatches().SingleOrDefault(l => l.Guid == listadoGuid);
      return View("ImprimirListado", listado);
    }      

    public ActionResult Enquiry()
    {
      var model = Dispatches();
      return View(model);
    }

    public ActionResult ExcelTemplate()
    {
      return File("~/Content/documents/Listado_Plantilla.xlsx", "application/vnd.ms-excel", "Listado_Plantilla.xlsx");
    }

    private DispatchModel GetAlbaran(UploadListadoModel model)
    {
      var result = new DispatchModel {Estado = "Received", Fecha = model.PedidoFecha.Value, Transportista = "KillerLogistics", Camion = model.CamionReferencia, PedidoReferencia = model.PedidoReferencia};
      var linea = new DispatchLineModel
        {
          LineaId = 1,
          TipoProducto = "Fresh",
          Producto = "Hake",
          Unidad = "Kg",
          Cantidad = 25,
          PuestoId = 18,
          Comerciante = "RedSquid"
        };

      result.Lineas.Add(linea);

      linea = new DispatchLineModel
      {
        LineaId = 2,
        TipoProducto = "Frozen",
        Producto = "Frozen Squid",
        Unidad = "Pallet",
        Cantidad = 4,
        PuestoId = 4,
        PuestoLetra = "A",
        Comerciante = "Alaska Brothers"
      };

      result.Lineas.Add(linea);

      linea = new DispatchLineModel
      {
        LineaId = 3,
        TipoProducto = "Shellfish",
        Producto = "Mussel",
        Unidad = "Sac",
        Cantidad = 20,
        PuestoId = 112,
        Comerciante = "Irish Seafoods"
      };

      result.Lineas.Add(linea);

      return result;
    }

    public ActionResult Confirm()
    {
      TempData["NotificationMsg"] = "Last dispatch note was confirmed";
      var listado = (DispatchModel) Session["Dispatch"];
      listado.FechaCreado = DateTime.Now;
      listado.Guid = Guid.NewGuid();
      listado.Usuario = User.Identity.Name;
      Dispatches().Add(listado);
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