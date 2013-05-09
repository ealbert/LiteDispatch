namespace LiteDispatch.Domain.Models
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

    public long HaulierId { get; set; }
  }
}