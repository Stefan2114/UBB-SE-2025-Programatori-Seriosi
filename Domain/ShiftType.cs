using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Domain
{
   public class ShiftType
    {
        public int ShiftTypeId { get; set; }
        public DateTime ShiftTypeStartTime { get; set; }
        public DateTime ShiftTypeEndTime { get; set; }

        public ShiftType(int shiftTypeId, DateTime shiftTypeStartTime, DateTime shiftTypeEndTime)
        {
            this.ShiftTypeId = shiftTypeId;
            this.ShiftTypeStartTime = shiftTypeStartTime;
            this.ShiftTypeEndTime = shiftTypeEndTime;
        }

    }

   
}
