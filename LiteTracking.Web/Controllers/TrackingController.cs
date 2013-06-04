using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LiteTracking.Web.Controllers
{
  using System.Configuration;
  using System.Runtime.Serialization.Json;
  using Models;
  using Models.BingMaps;

  public class TrackingController : ApiController
  {
    [HttpGet]
    public TrackingResponse GetRouteDetails(string truckRegistration, double latitude, double longitude)
    {
      var response = new TrackingResponse
        {
          Latitude = latitude,
          Longitude = longitude
        };

      const string query = @"http://dev.virtualearth.net/REST/V1/Routes/Driving?o=json&wp.0={0},{1}&wp.1={2},{3}&optmz=distance&rpo=Points&key={4}";
      var origin = new[] {40.631740, -4.003754};
      var  uri = new Uri(string.Format(query, latitude, longitude, origin[0], origin[1], GetKey()));

      var bingResponse = GetRouteDetails(uri);
      if (bingResponse.ResourceSets.Any() && bingResponse.ResourceSets[0].Resources.Any())
      {
        var routeDetails = bingResponse.ResourceSets[0].Resources[0] as Route;
        if (routeDetails != null)
        {
          response.Distance = routeDetails.TravelDistance;
          response.Eta = routeDetails.TravelDuration;
          response.DistanceMetric = routeDetails.DistanceUnit;
          response.EtaMetric = routeDetails.DurationUnit;
        }
      }
      return response;
    }

    private Response GetRouteDetails(Uri uri)
    {
      var wc = new WebClient();
      var response = wc.OpenRead(uri);
      var ser = new DataContractJsonSerializer(typeof (Response));
      if (response != null)
      {
        return ser.ReadObject(response) as Response;
      }
      return new Response();
    }

    private static string _bingMapsKey;

    private string GetKey()
    {
      if (_bingMapsKey != null) return _bingMapsKey;
      _bingMapsKey = ConfigurationManager.AppSettings["BingMapsKey"];
      return _bingMapsKey;
    }
  }
}
