using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{
    public class Patient 
    {
        public int UserId { get; set; }
        public Patient(int userId)
        {
            this.UserId = userId;
        }
    }
}
