using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Domain
{
    public class Room
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }

        public Room(int id, int departmentId)
        {
            this.Id = id;
            this.DepartmentId = departmentId;
        }
    }

   
}
