using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User:Entity
    {
        public string Name { get; set; }
        public string SurName { get; set; }

        public Bug Bug { get; set; }
    }
}
