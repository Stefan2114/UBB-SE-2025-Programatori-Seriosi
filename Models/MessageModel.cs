﻿using System;
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
            const string query = "SELECT id, content, user_id, chat_id FROM messages WHERE chat_id = @chat_id";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@chat_id", chatId);
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
                                userId: reader.GetInt32(2),
                                chatId: reader.GetInt32(3)
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

        public void addMessage(int userId, int chatId, string content)
        {
            const string query = "INSERT INTO messages (content, use_iId, chat_id) VALUES (@content, @userId, @chatId)";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@content", content);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@chat_id", chatId);

                    connection.Open();

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {
                throw new Exception("Error while adding message: " + e.Message);
            }
        }

    }
}
