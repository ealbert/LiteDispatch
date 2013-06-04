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
    public double Eta { get; set; }
    public string EtaMetric { get; set; }
  }
}