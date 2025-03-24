using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{
    public class Hospitalization
    {
        public int HospitalizationId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Hospitalization(int hospitalizationId, DateTime startDate, DateTime endDate)
        {
            this.HospitalizationId = hospitalizationId;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
