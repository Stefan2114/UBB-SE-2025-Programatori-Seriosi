using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }

        public Doctor(int id, int userId, int departmentId)
        {
            Id = id;
            UserId = userId;
            DepartmentId = departmentId;
        }
    }
}
