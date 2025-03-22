using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Domain
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int DepartmentId { get; set; }

        public Room(int roomId, string roomName, int departmentId)
        {
            this.RoomId = roomId;
            this.RoomName = roomName;
            this.DepartmentId = departmentId;
        }
    }

   
}
