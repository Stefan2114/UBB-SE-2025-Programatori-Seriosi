using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Team3.Entities;
using System.Diagnostics;

namespace Team3.Models
{
    public class MessageModel
    {
        private static MessageModel? _instance;
        private readonly Config _config;
        private Task<List<Message>> _messages;
        private static readonly object _lock = new object();

        private MessageModel()
        {
            _config = Config.Instance;
        }

        public static MessageModel Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MessageModel();
                    }   
                }
                return _instance;
            }
        }

        public List<Message> GetMessagesByChatId(int chatId)
        {
            Console.WriteLine($"Attempting to connect to: {Config.CONNECTION}");
            const string query = "SELECT message_id, content, userId, chatId FROM messages WHERE chatId = @chatId";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@chatId", chatId);
                    Debug.WriteLine("the chat it is:" +  chatId);

                    connection.Open();

                    List<Message> messages = new List<Message>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            messages.Add(new Message(
                                id: reader.GetInt32(0),
                                content: reader.GetString(1),
                                user_id: reader.GetInt32(2),
                                chat_id: reader.GetInt32(3)
                            ));
                        }
                    }

                    return messages;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error while loading messages: " + e.Message);
            }
        }

        public int addMessage(int userId, int chatId, string content)
        {
            const string query = "INSERT INTO messages (content, userId, chatId) OUTPUT INSERTED.message_id VALUES (@content, @userId, @chatId)";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@content", content);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@chatId", chatId);

                    connection.Open();

                    int newMessageId = (int)command.ExecuteScalar(); // Get the newly inserted message_id

                    Debug.WriteLine($"New message inserted with ID: {newMessageId}");
                    return newMessageId;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error while adding message: " + e.Message);
            }
        }

    }
}
