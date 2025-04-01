using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.DTOs
{

    /// <summary>
    ///sper ca e ok asta 
    /// </summary>
    internal class DepartmentDTO
    {

        public string Name { get; set; }
        public int DoctorCount { get; set; }
        public int EquipmentCount { get; set; }
        public int RoomCount { get; set; }
        public int InterneeCount { get; set; }

        public DepartmentDTO(string name, int doctorCount, int equipmentCount, int roomCount, int interneeCount)
        {
            Name = name;
            DoctorCount = doctorCount;
            EquipmentCount = equipmentCount;
            RoomCount = roomCount;
            InterneeCount = interneeCount;
        }
    }
}
