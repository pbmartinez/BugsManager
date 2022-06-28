using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Bug : Entity
    {
        public Bug()
        {
            CreationDate = DateTime.UtcNow;
        }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreationDate { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }


    }
}
