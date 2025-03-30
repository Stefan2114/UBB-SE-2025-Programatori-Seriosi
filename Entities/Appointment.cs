using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{
    public class Appointment
    {
        public int id { get; set; }
        public int doctorId { get; set; }
        public int patientId { get; set; }
        public DateTime appointmentDate { get; set; }
        public string location { get; set; }

        override public string ToString()
        {
            return $"Appointment(Id: {id}, DoctorId: {doctorId}, PatientId: {patientId}, AppointmentDate: {appointmentDate}, Location: {location})";
        }

    }
}
