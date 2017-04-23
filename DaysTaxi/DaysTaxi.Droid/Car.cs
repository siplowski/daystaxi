using Newtonsoft.Json;
using Kinvey;

namespace DaysTaxi.Droid
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Car : Entity
    {
        public Car() { }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photo_id")]
        public string PhotoID { get; set; }

        [JsonProperty("class_id")]
        public int Class { get; set; }

        [JsonProperty("_kmd")]
        public KinveyMetaData Meta { get; set; }

        
        [JsonProperty("_acl")]
        public KinveyMetaData acl { get; set; }

        [JsonProperty("_id")]
        public int id { get; set; }

    }
}