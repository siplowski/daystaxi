using Kinvey;
using Newtonsoft.Json;

namespace DaysTaxi.Droid
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CarClass:Entity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

    }
}