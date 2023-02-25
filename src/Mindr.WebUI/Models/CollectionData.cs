using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mindr.WebUI.Models;

public partial class CollectionData
{

    public CollectionInfo info { get; set; }

    [JsonProperty("item")]
    public HttpOverview[] Overview { get; set; }

    public Variable[] variable { get; set; }

}
