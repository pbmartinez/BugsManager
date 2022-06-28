using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Project : Entity
    {
        public Project()
        {
            Bugs = new HashSet<Bug>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        
        public virtual ICollection<Bug> Bugs { get; set; }
    }
}
