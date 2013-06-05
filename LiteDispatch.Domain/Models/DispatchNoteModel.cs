namespace LiteDispatch.Domain.Models
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using Entities;

  public class DispatchNoteModel
  {
    public DispatchNoteModel()
    {
      Lines = new List<DispatchLineModel>();
    }

    public long Id { get; set; }
    

    [Display(Name = "Dispatch Date")]
    [DataType(DataType.Date)]
    public DateTime DispatchDate { get; set; }

    [Display(Name = "State")]
    public DispatchNoteStatusEnum  DispatchNoteStatus { get; set; }
        
    [Display(Name = "Truck Reg#")]
    public string TruckReg { get; set; }

    [Display(Name = "Reference Number")]
    public string DispatchReference { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreationDate { get; set; }        
    
    public string User { get; set; }
    public long HaulierId { get; set; }
    public string HaulierName { get; set; }
    public List<DispatchLineModel> Lines { get; set; }

    public string Distance { get; set; }
    public string Duration { get; set; }
  }
}