using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.DTOs
{
    //ca doctoru normal + no of hrs worked and number of pacients treated   
    internal class DoctorDTO
    {
       // public int Id { get; set; }
        public string Name { get; set; }
        public int TotalHoursWorked { get; set; }
        public int PatientsTreated { get; set; }

        public DoctorDTO( string name, int totalHoursWorked, int patientsTreated)
        {
            //Id = id;
            Name = name;
            TotalHoursWorked = totalHoursWorked;
            PatientsTreated = patientsTreated;
        }
    }
}
