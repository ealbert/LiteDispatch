using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteDispatch.Web.Models
{
  using System.ComponentModel.DataAnnotations;
  using Domain.Entities;

  public class PascalRequestModel
  {
    [Required]
    [Range(0, 32, ErrorMessage = "Valid levels are between 0 and 32")]
    [Display(Name = "Level To Calculate")]
    public int Level { get; set; }

    public List<PascalRecord> PascalRecords { get; set; }

    public string ElapsedTime { get; set; }
  }
}