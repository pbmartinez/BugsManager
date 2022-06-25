using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Project : Entity
    {
        public Project()
        {
            Bugs = new HashSet<Bug>();
        }

        public string Name { get; set; } 
        public string Description { get; set; }

        
        public virtual ICollection<Bug> Bugs { get; set; }
    }
}
