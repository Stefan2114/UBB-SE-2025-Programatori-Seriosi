using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string role { get; set; }

        public User(int id, string username, string role)
        {
            this.id = id;
            this.username = username;
            this.role = role;
        }
    }
}
