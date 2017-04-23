using Newtonsoft.Json;
using Kinvey;


namespace DaysTaxi.Droid
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Book : Entity
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}

