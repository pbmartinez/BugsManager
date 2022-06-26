using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class ProjectDto : Entity
    {
        public ProjectDto()
        {
            Bugs = new HashSet<BugDto>();
        }

        public string Name { get; set; } 

        public string Description { get; set; }

        
        public virtual ICollection<BugDto> Bugs { get; set; }
    }
}
