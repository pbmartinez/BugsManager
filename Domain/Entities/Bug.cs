using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class Bug : Entity
    {

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        
        public DateTime CreationDate { get; }



        public int UserId { get; set; }
        public User User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }


    }
}
