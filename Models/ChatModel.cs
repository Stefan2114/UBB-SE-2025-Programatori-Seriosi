using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;

namespace Team3.Models
{
    public class ChatModel
    {
        private static ChatModel? _instance;
        private readonly Config _config;
        private Task<List<Chat>> _chats;
        private static readonly object _lock = new object();

        private ChatModel()
        {
            _config = Config.Instance;
        }

        public static ChatModel Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ChatModel();
                    }
                }
                return _instance;
            }
        }

        public List<Chat> getChats(int userId)
        {
            const string query = "SELECT * FROM Chats WHERE user1 = @userId OR user2 = @userId";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        List<Chat> chats = new List<Chat>();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                chats.Add(new Chat((int)reader["ChatID"], (int)reader["user1"], (int)reader["user2"]));
                            }
                        }
                        Debug.WriteLine(chats.Count + " chats loaded");
                        return chats;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting chats", e);
            }
        }

        public void addChat(int user1, int user2)
        {
            const string query = "INSERT INTO chats (user1, user2) VALUES (@user1, @user2)";
            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@user1", user1);
                command.Parameters.AddWithValue("@user2", user2);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error adding chat", e);
            }
        }
    }
}
