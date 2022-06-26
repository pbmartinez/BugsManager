using System;
using System.Text.Json.Serialization;

namespace WebApi.Parameters
{
    public class BugQueryStringParameters
    {
        [JsonPropertyName("project_id")]
        public int? ProjectId { get; set; }

        [JsonPropertyName("user_id")]
        public int? UserId { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }

        public bool IsValid => !(ProjectId == null && UserId == null && StartDate == null && EndDate == null);
    }
}
