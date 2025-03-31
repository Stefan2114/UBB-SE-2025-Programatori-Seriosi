using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Domain
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Department(int departmentId, string departmentName)
    {
        this.DepartmentId = departmentId;
        this.DepartmentName = departmentName;
    }

    }

   
}
