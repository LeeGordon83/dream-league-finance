using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LG.DLFinance.Models
{
    public class MeetingAPI
    {
        [JsonProperty(PropertyName = "MeetingId")]
        public int MeetingId { get; set; }

        [JsonProperty(PropertyName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public String MeetingDate { get; set; }

        [JsonProperty(PropertyName = "Location")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public String Location { get; set; }

    }
}