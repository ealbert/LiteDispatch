using System;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Bing.Maps;
using LiteTracker.UI.DataModel;
using LiteTracking.Web.Models.BingMaps;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Location = Bing.Maps.Location;

namespace LiteTracker.UI.Services
{
  internal class MapServices
  {

    public MapServices(Map map, Uri uri)
    {
      _routeLayer = new MapShapeLayer();
      _mapTrucks = map;
      _mapTrucks.ShapeLayers.Add(_routeLayer);
      _baseUri = uri;
    }

    private readonly MapShapeLayer _routeLayer;
    private readonly Map _mapTrucks;
    private readonly Uri _baseUri;

    public void RefreshMap(DispatchNoteSummary selectedItem)
    {
      var location = new Location(selectedItem.Latitude, selectedItem.Longitude);
      if (location.Latitude == 0 && location.Longitude == 0) return;

      SetRoute(location, new Location(52.516071, 13.37698));
      var image = new Image
        {
          Source = new BitmapImage(new Uri(_baseUri, "/images/truck_map.png")),
          Width = 40,
          Height = 40
        };

      MapLayer.SetPosition(image, location);
      _mapTrucks.SetView(location);
      _mapTrucks.Children.Add(image);
    }

    private async Task<Response> GetResponse(Uri uri)
    {
      var client = new System.Net.Http.HttpClient();
      var response = await client.GetAsync(uri);

      using (var stream = await response.Content.ReadAsStreamAsync())
      {
        var ser = new DataContractJsonSerializer(typeof (Response));
        return ser.ReadObject(stream) as Response;
      }
    }

    private void ClearMap()
    {
      _mapTrucks.Children.Clear();
      _routeLayer.Shapes.Clear();
    }

    private async void SetRoute(Location startLocation, Location endLocation)
    {
      ClearMap();
      //Create the Request URL for the routing service
      const string request =
        @"http://dev.virtualearth.net/REST/V1/Routes/Driving?o=json&wp.0={0},{1}&wp.1={2},{3}&rpo=Points&key={4}";

      var routeRequest =
        new Uri(string.Format(request, startLocation.Latitude, startLocation.Longitude, endLocation.Latitude,
                              endLocation.Longitude, _mapTrucks.Credentials));

      //Make a request and get the response
      var r = await GetResponse(routeRequest);

      if (r != null &&
          r.ResourceSets != null &&
          r.ResourceSets.Length > 0 &&
          r.ResourceSets[0].Resources != null &&
          r.ResourceSets[0].Resources.Length > 0)
      {
        var route = r.ResourceSets[0].Resources[0] as Route;
        if (route == null) return;

        //Get the route line data
        var routePath = route.RoutePath.Line.Coordinates;
        var locations = new LocationCollection();

        foreach (var t in routePath)
        {
          if (t.Length >= 2)
          {
            locations.Add(new Location(t[0], t[1]));
          }
        }

        //Create a MapPolyline of the route and add it to the map
        var routeLine = new MapPolyline
          {
            Color = Colors.Blue,
            Locations = locations,
            Width = 5
          };

        _routeLayer.Shapes.Add(routeLine);

        //Add start and end pushpins
        var start = new Pushpin
          {
            Text = "S",
            Background = new SolidColorBrush(Colors.Green)
          };

        _mapTrucks.Children.Add(start);
        MapLayer.SetPosition(start,
                             new Location(route.RouteLegs[0].ActualStart.Coordinates[0],
                                          route.RouteLegs[0].ActualStart.Coordinates[1]));

        var end = new Pushpin
          {
            Text = "E",
            Background = new SolidColorBrush(Colors.Red)
          };

        _mapTrucks.Children.Add(end);
        MapLayer.SetPosition(end,
                             new Location(route.RouteLegs[0].ActualEnd.Coordinates[0],
                                          route.RouteLegs[0].ActualEnd.Coordinates[1]));

        //Set the map view for the locations
        var locationRect = new LocationRect(locations);
        locationRect.Width += 0.5;
        locationRect.Height += 0.5;
        _mapTrucks.SetView(locationRect);
      }
    }
  }
}
