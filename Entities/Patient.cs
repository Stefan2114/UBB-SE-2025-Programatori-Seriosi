using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{
    public class Patient : User
    {
        public int id { get; set; }
        public Patient(int id, string username, string role) : base(id, username, role)
        {
            this.id = id;
        }
    }
}
