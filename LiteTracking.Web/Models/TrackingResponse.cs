using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteTracking.Web.Models
{
  public class TrackingResponse
  {
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Distance { get; set; }
    public string DistanceMetric { get; set; }
    public double Duration { get; set; }
    public string DurationMetric { get; set; }
    public string TruckRegistration { get; set; }
    public bool NotificationWasCreated { get; set; }
    public Guid RequestGuid { get; set; }
    public long DispatchNoteId { get; set; }
    public string Error { get; set; }
  }
}