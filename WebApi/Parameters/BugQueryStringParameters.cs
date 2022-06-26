using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace WebApi.Parameters
{
    public class BugQueryStringParameters
    {
        [JsonProperty(PropertyName = "project_id")]
        public int? ProjectId { get; set; }
                
        [JsonProperty(PropertyName = "user_id")]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "start_date")]
        public DateTime? StartDate { get; set; }

        [JsonProperty(PropertyName = "end_date")]
        public DateTime? EndDate { get; set; }

        public bool IsValid => !(ProjectId == null && UserId == null && StartDate == null && EndDate == null);
    }
}
