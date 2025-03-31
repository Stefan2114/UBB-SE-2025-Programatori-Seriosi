using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{
    public class Hospitalization
    {
<<<<<<< HEAD
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; } // Nullable for potential null values

        public Hospitalization(int id, int roomId, DateTime startDate, DateTime? endDate)
        {
            Id = id;
            RoomId = roomId;
            StartDate = startDate;
            EndDate = endDate;
=======


        public int Id { get; set; }

        public int RoomId { get; set; }

        public int PatientId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }


        public Hospitalization(int id, int roomId, int patientId, DateTime startDateTime, DateTime endDateTime)
        {
            Id = id;
            RoomId = roomId;
            PatientId = patientId;
            StartDateTime = endDateTime;
            EndDateTime = endDateTime;
>>>>>>> fc43f03d383be3c59041616655af9c9442476244
        }

    }

}
