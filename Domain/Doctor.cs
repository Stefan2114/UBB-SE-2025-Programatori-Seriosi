using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Domain
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string user_id { get; set; }


        public Doctor(int doctorId, string user_id)
        {
            this.DoctorId = doctorId;
            this.user_id = user_id;
        }
    }

  

}
