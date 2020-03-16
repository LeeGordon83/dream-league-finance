using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LG.DLFinance.Models
{
    public class WeeklyAPI
    {

        [JsonProperty(PropertyName = "GameWeek")]
        public int WeekNo { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public String Winner { get; set; }
    }
}