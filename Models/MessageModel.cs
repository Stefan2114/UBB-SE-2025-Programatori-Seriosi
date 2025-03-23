using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Team3.Entities;

namespace Team3.Models
{
    class MessageModel
    {
        private static MessageModel? _instance;
        private readonly Config _config;
        private Task<List<Message>> _messages;

        private MessageModel()
        {
            _config = Config.Instance;
        }

        public static MessageModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MessageModel();
                }
                return _instance;
            }
        }

        public List<Message> getMessagesByChatId(int chatId)
        {
            const string query = "SELECT * FROM messages WHERE chat_id = @chatId";

            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                List<Message> messages = new List<Message>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string content = reader.GetString(1);
                        int uer_id = reader.GetInt32(2);
                        int chat_id = reader.GetInt32(3);
                        Message message = new Message(id, content, uer_id, chat_id);
                        messages.Add(message);
                    }
                }

                connection.Close();

                return messages;
            }
            catch (Exception e)
            {
                throw new Exception("Error while connecting to the database: " + e.Message);
            }
        }

        public void addMessage(Message message)
        {
            string query = "INSERT INTO messages (message_id, content, user_id, chat_id) VALUES (@message_id, @content, @userId, @chatId)";

            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@message_id", message.id);
                command.Parameters.AddWithValue("@content", message.content);
                command.Parameters.AddWithValue("@userId", message.user_id);
                command.Parameters.AddWithValue("@chatId", message.chat_id);

                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error while connecting to the database: " + e.Message);
            }
        }
    }
}
