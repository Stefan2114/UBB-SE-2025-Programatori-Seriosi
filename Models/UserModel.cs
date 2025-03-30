using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Team3.Entities;

namespace Team3.Models
{
    public class UserModel
    {

        private static UserModel? _instance;
        private readonly Config _config;
        private static readonly object _lock = new object();
        private UserModel()
        {
            _config = Config.Instance;
        }

        public static UserModel Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new UserModel();
                    }   
                }
                return _instance;
            }   
        }


        public List<User> GetUsers()
        {
            const string query = "SELECT * FROM users;";
            List<User> notifications = new List<User>();

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notifications.Add(new User(
                                (int)reader[0],
                                reader[1].ToString(),
                                reader[2].ToString()
                            ));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving notifications", e);
            }

            return notifications;
        }
    }


}
