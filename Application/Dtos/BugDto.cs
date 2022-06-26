using Domain.Entities;
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

        [JsonPropertyName("user")]
        [Required]
        public int UserId { get; set; }

        [JsonPropertyName("project")]
        [Required]
        public int ProjectId { get; set; }


    }
}
