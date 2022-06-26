using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos
{
    public class UserDto : Entity
    {
        public string Name { get; set; }
        public string SurName { get; set; }

        public BugDto Bug { get; set; }
    }
}
