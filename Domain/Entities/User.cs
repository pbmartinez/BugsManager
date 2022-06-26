using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class User:Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }

        public Bug Bug { get; set; }
    }
}
