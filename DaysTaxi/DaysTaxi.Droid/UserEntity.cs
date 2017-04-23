using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Newtonsoft.Json;
using Kinvey;

namespace DaysTaxi.Droid
{
    [JsonObject(MemberSerialization.OptIn)]
    class UserEntity:Kinvey.Entity
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}