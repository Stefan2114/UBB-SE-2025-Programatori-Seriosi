using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Entities;

namespace Team3.Models
{
    public class NotificationModel
    {
        private static NotificationModel? _instance;
        private readonly Config _config;

        private NotificationModel()
        {
            _config = Config.Instance;
        }

        public static NotificationModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotificationModel();
                }
                return _instance;
            }
        }

        public List<Notification> GetNotifications()
        {
            const string query = "SELECT * FROM Notifications;";
            List<Notification> notifications = new List<Notification>();

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
                            notifications.Add(new Notification(
                                (int)reader["Id"],
                                (DateTime)reader["DeliveryDateTime"],
                                reader["Message"].ToString()
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

        public void AddNotification(Notification notification)
        {
            const string query = "INSERT INTO Notifications (Id, DeliveryDateTime, Message) VALUES (@Id, @DeliveryDateTime, @Message);";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", notification.Id);
                    command.Parameters.AddWithValue("@DeliveryDateTime", notification.DeliveryDateTime);
                    command.Parameters.AddWithValue("@Message", notification.Message);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error adding notification", e);
            }
        }
    }
}
