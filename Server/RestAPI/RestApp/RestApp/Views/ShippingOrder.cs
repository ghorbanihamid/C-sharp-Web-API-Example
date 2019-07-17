
using Newtonsoft.Json;

namespace RestApp.Views
{
    public class ShippingOrder
    {
        [JsonProperty("SourceAddress")]
        SourceAddress SourceAddress { get; set; }

        [JsonProperty("TargetAddress")]
        TargetAddress TargetAddress { get; set; }

        [JsonProperty("PackageDimension")]
        PackageDimension PackageDimension { get; set; }
    }
}