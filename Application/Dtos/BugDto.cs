using Application.IAppServices;
using Domain.Entities;
using Domain.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;



namespace Application.Dtos
{
    public class BugDto : Entity, IValidatableObject
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
        //[JsonProperty(PropertyName = "user")]
        public int UserId { get; set; }
   
        [Required]
        //[JsonProperty(PropertyName = "project")]     
        public int ProjectId { get; set; }

        public string UserFullName { get; set; }

        public string ProjectName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (UserId <= 0)
                errors.Add(new ValidationResult(Resource.validation_FieldRequired, new string[] { nameof(UserId) }));
            if (ProjectId <= 0)
                errors.Add(new ValidationResult(Resource.validation_FieldRequired, new string[] { nameof(ProjectId) }));
            return errors;
        }
    }
}
