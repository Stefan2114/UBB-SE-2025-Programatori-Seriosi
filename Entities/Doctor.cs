using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{

    /// class Doctor
    /// a type of User with a specified role
    /// Input: int id, string username, string role
    /// </summary>contains a private int id coming from user
    public class Doctor : User
    {
        private int id;

        public Doctor(int id, string username, string role)
            /// <summary>Constructor Doctor
            : base(id, username, role)
        {
            this.id = id;
        }
    }

}
