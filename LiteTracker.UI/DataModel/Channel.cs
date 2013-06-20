using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LiteTracker.UI.DataModel
{
  public class Channel
  {
    public int Id { get; set; }

    [JsonProperty(PropertyName = "uri")]
    public string Uri { get; set; }
  }
}
