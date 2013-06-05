namespace LiteTracking.Web.Controllers
{
  using System;
  using System.IO;
  using System.Net;

  /// <summary>
  /// Basic REST client that invokes a GET to the given endpoint
  /// </summary>
  public class RestClient
  {
    /// <summary>
    /// Full URI to REST service
    /// </summary>
    public string EndPoint { get; private set; }

    /// <summary>
    /// Construct Endpoint Uri from host, service, method and parameters
    /// </summary>
    public RestClient(string host, string service, string method, string parameters)
    {
      EndPoint = string.IsNullOrEmpty(parameters)
                   ? string.Format("{0}/{1}/{2}", host, service, method)
                   : string.Format("{0}/{1}/{2}?{3}", host, service, method, parameters);

    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="endpoint">Full URI to invoke by the client</param>
    public RestClient(string endpoint)
    {
      EndPoint = endpoint;
    }

    /// <summary>
    /// Issues a GET request to the specified endpoint, expects
    /// the context type to be text/xml
    /// </summary>
    /// <exception cref="ApplicationException">It may throw application exception if the request fails</exception>
    /// <returns>Response in string format</returns>
    public string MakeRequest()
    {
      var request = (HttpWebRequest)WebRequest.Create(EndPoint);
      request.Method = "GET";
      request.ContentLength = 0;
      request.ContentType = "text/xml";

      // the following line fails if the server is not running !!!
      using (var response = (HttpWebResponse)request.GetResponse())
      {
        var responseValue = string.Empty;
        if (response.StatusCode != HttpStatusCode.OK)
        {
          var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
          throw new ApplicationException(message);
        }

        // grab the response
        using (var responseStream = response.GetResponseStream())
        {
          if (responseStream != null)
          {
            using (var reader = new StreamReader(responseStream))
            {
              responseValue = reader.ReadToEnd();
            }
          }
        }
        return responseValue;
      }
    }

  }
}