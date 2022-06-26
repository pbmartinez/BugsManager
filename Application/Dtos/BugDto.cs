using Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;


namespace Application.Dtos
{
    public class BugDto : Entity
    {
        public BugDto()
        {
            CreationDate = DateTime.UtcNow;
        }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        
        [Required]
        [JsonProperty(PropertyName = "user")]
        public int UserId { get; set; }
   
        [Required]
        [JsonProperty(PropertyName = "project")]     
        public int ProjectId { get; set; }


    }
}
