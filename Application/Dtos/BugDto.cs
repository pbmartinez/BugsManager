using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class BugDto : Entity
    {
        public BugDto()
        {
            CreationDate = DateTime.UtcNow;
        }

        //Max 100
        public string Description { get; set; }
        
        //if advanced .net =>  { get; init; }
        public DateTime CreationDate { get; }



        public Guid UserId { get; set; }
        public UserDto User { get; set; }

        public Guid ProjectId { get; set; }
        public ProjectDto Project { get; set; }


    }
}
