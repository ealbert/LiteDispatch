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
  using LiteDispatch.Core.DTOs;
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

      ProcessRequest(truckRegistration, latitude, longitude, response);
      return response;
    }

    private void ProcessRequest(string truckRegistration, double latitude, double longitude, TrackingResponse response)
    {
      const string query =
        @"http://dev.virtualearth.net/REST/V1/Routes/Driving?o=json&wp.0={0},{1}&wp.1={2},{3}&optmz=distance&rpo=Points&key={4}";
      var origin = new[] {52.516071, 13.37698};
      var uri = new Uri(string.Format(query, latitude, longitude, origin[0], origin[1], GetKey()));

      var bingResponse = GetRouteDetails(uri);
      if (bingResponse.ResourceSets.Any() && bingResponse.ResourceSets[0].Resources.Any())
      {
        var routeDetails = bingResponse.ResourceSets[0].Resources[0] as Route;
        if (routeDetails != null)
        {
          response.Distance = routeDetails.TravelDistance;
          response.Duration = routeDetails.TravelDuration;
          response.DistanceMetric = routeDetails.DistanceUnit;
          response.DurationMetric = routeDetails.DurationUnit;
          response.TruckRegistration = truckRegistration;

          SendNotification(response);
        }
      }
    }

    private void SendNotification(TrackingResponse response)
    {
      using (var client = new WebClient())
      {
        client.Headers[HttpRequestHeader.ContentType] = "application/json";
        var notificationDto = new TrackingNotificationDto
          {
            Distance = response.Distance,
            DistanceMetric = response.DistanceMetric,
            Duration = response.Duration,
            DurationMetric = response.DurationMetric,
            Latitude = response.Latitude,
            Longitude = response.Longitude,
            TruckRegistration = response.TruckRegistration,
            Id = Guid.NewGuid()
          };

        var json = Newtonsoft.Json.JsonConvert.SerializeObject(notificationDto);
        var result = client.UploadString(GetTrackingNotificationUri(), json);
        var dto = Newtonsoft.Json.JsonConvert.DeserializeObject<TrackingResponseDto>(result);
        response.NotificationWasCreated = dto.Accepted;
        response.RequestGuid = dto.NotificationId;
        response.DispatchNoteId = dto.DispatchNoteId;
        response.Error = dto.Error;
        var r = dto;
      }
    }

    private static string _trackingNotificationUri;
    
    private string GetTrackingNotificationUri()
    {
      if (_trackingNotificationUri != null) return _trackingNotificationUri;
      _trackingNotificationUri = string.Format("http://{0}/api/tracking/CreateTrackingNotification", ConfigurationManager.AppSettings["TrackingNotificationHost"]);
      return _trackingNotificationUri;
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
