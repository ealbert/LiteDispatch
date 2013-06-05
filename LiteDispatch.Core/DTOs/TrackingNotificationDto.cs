using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteDispatch.Core.DTOs
{
  public class TrackingNotificationDto
  {
    public Guid Id { get; set; }
    public string TruckRegistration { get; set; }
    public long DispatchNoteId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Distance { get; set; }
    public string DistanceMetric { get; set; }
    public double Eta { get; set; }
    public string EtaMetric { get; set; }
  }
}
