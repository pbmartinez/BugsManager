using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Bug : Entity
    {

        //Max 100
        public string Description { get; set; }
        
        //if advanced .net =>  { get; init; }
        public DateTime CreationDate { get; set; }



        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }


    }
}
