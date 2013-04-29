namespace LiteDispatch.Web.Models
{
  using System;
  using System.ComponentModel.DataAnnotations;

  public class UploadListadoModel
  {
    [Required]
    [StringLength(30, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 3)]
    [DataType(DataType.Text)]
    [Display(Name = "Reference Number")]
    public string PedidoReferencia { get; set; }

    [Required]
    [StringLength(15, ErrorMessage = "The {0} field must be between {2} and {1} characters", MinimumLength = 6)]
    [DataType(DataType.Text)]
    [Display(Name = "Truck Reg#")]
    public string CamionReferencia { get; set; }

    [Required]
    [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]    
    [Display(Name = "Dispatch Date")]
    public DateTime? PedidoFecha { get; set; }

  }
}