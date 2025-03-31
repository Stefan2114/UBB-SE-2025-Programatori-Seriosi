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
        public TimeOnly ShiftTypeStartTime { get; set; }
        public TimeOnly ShiftTypeEndTime { get; set; }

        public ShiftType(int shiftTypeId, TimeOnly shiftTypeStartTime, TimeOnly shiftTypeEndTime)
        {
            this.ShiftTypeId = shiftTypeId;
            this.ShiftTypeStartTime = shiftTypeStartTime;
            this.ShiftTypeEndTime = shiftTypeEndTime;
        }

    }

   
}
