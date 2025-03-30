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
    public class Doctor
    {
        private int userId;

        public Doctor(int userId)
        {
            this.userId = userId;
        }
    }

}
