using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team3.Entities
{
    class Message
    {
        public int id { get; set; }
        public string content { get; set; }
        public int user_id { get; set; }
        public int chat_id { get; set; }

        public Message(int id, string content, int user_id, int chat_id)
        {
            this.id = id;
            this.content = content;
            this.user_id = user_id;
            this.chat_id = chat_id;
        }
    }
}
