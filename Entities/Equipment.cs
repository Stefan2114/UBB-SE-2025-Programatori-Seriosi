using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string EquipmentModel { get; set; }

        public Equipment(int equipmentId, string equipmentName, string equipmentModel)
        {
            this.EquipmentId = equipmentId;
            this.EquipmentName = equipmentName;
            this.EquipmentModel = equipmentModel;
        }
    }
}
