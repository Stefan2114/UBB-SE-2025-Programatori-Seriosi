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
        public string DoctorName { get; set; }


        public Doctor(int doctorId, string doctorName)
        {
            this.DoctorId = doctorId;
            this.DoctorName = doctorName;
        }
    }

  

}
