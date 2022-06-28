using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos
{
    public class UserDto : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }

        public BugDto Bug { get; set; }


        public string FullName => $"{Name} {SurName}";
    }
}
