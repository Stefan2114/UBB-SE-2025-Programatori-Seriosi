using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.DTOs
{
    internal class ChatDTO
    {
        public int ChatID { get; set; }
        public int user1 { get; set; }
        public int user2 { get; set; }
        public string userName1 { get; set; }
        public string userName2 { get; set; }

        public ChatDTO(int chatID, int user1, int user2, string userName1, string userName2)
        {
            ChatID = chatID;
            this.user1 = user1;
            this.user2 = user2;
            this.userName1 = userName1;
            this.userName2 = userName2;
        }
    }
}
