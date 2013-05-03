namespace LiteDispatch.Web.Models
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class DispatchModel
  {
    public DispatchModel()
    {
      Lines = new List<DispatchLineModel>();
    }

    public string Transportista { get; set; }

    [Display(Name = "Dispatch Date")]
    [DataType(DataType.Date)]
    public DateTime DispatchDate { get; set; }

    [Display(Name = "State")]
    public string State { get; set; }
        
    [Display(Name = "Truck Reg#")]
    public string TruckReg { get; set; }

    [Display(Name = "Reference Number")]
    public string DispatchReference { get; set; }

    public List<DispatchLineModel> Lines { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreationDate { get; set; }

    public Guid Guid { get; set; }
    
    public string User { get; set; }

  }

  public class DispatchLineModel
  {
    public int LineId { get; set; }
    public string ProductType { get; set; }
    public string Product { get; set; }
    public string Metric { get; set; }
    public int Quantity { get; set; }
    public int ShopId { get; set; }
    public string ShopLetter { get; set; }
    public string Client { get; set; }

    public string ShopTitle()
    {
      return string.IsNullOrEmpty(ShopLetter)
               ? ShopId.ToString()
               : string.Format("{0}-{1}", ShopId, ShopLetter);
    }
  }
}