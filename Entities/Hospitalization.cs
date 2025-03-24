using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{
    public class Hospitalization
    {
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
        }
    }

}
