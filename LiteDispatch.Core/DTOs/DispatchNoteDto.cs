namespace LiteDispatch.Core.DTOs
{
  using System;
  using System.Collections.Generic;

  public class DispatchNoteDto
  {
    public long Id { get; set; }
    public string Haulier { get; set; }
    public string DispatchStatus { get; set; }
    public string TruckReg { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdate { get; set; }
    public string LastTrackingNotification { get; set; }
    public ICollection<DispatchLineDto> DispatchLineSet { get; set; }
  }
}