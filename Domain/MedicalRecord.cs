using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Domain
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }
        public int DoctorId { get; set; }
        public MedicalRecord(int medicalRecordId, int doctorId)
        {
            this.MedicalRecordId = medicalRecordId;
            this.DoctorId = doctorId;
        }
        override
        public string ToString()
        {
            return $"Id: {MedicalRecordId}, doctor: {DoctorId}";
        }
    }

    

}

