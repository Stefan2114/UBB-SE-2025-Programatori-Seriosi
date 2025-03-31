using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Domain
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int Patient_id { get; set; }

        public DateOnly recordDate {  get; set; }
        public MedicalRecord(int id, int doctorId, int patient_id, DateOnly recordDate)
        {
            this.Id = id;
            this.DoctorId = doctorId;
            this.Patient_id = patient_id;
            this.recordDate = recordDate;
        }
        override
        public string ToString()
        {
            return $"Id:{Id}, doctor: {DoctorId}";
        }
    }

    

}

