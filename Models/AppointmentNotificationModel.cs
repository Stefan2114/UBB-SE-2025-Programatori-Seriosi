using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Entities;

namespace Team3.Models
{
    public class AppointmentNotificationModel
    {
        private static AppointmentNotificationModel? _instance;
        private readonly Config _config;

        private AppointmentNotificationModel()
        {
            _config = Config.Instance;
        }

        public static AppointmentNotificationModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppointmentNotificationModel();
                }
                return _instance;
            }
        }

        public void AddAppointmentNotification(AppointmentNotification notification)
        {
            const string query = "INSERT INTO AppointmentNotifications (Id, DeliveryDateTime, Message, AppointmentId) VALUES (@Id, @DeliveryDateTime, @Message, @AppointmentId);";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", notification.Id);
                    command.Parameters.AddWithValue("@DeliveryDateTime", notification.DeliveryDateTime);
                    command.Parameters.AddWithValue("@Message", notification.Message);
                    command.Parameters.AddWithValue("@AppointmentId", notification.AppointmentId);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error adding appointment notification", e);
            }
        }

        public List<AppointmentNotification> GetUpcomingAppointmentNotifications()
        {
            const string query = "SELECT * FROM AppointmentNotifications WHERE DeliveryDateTime >= GETDATE();";
            List<AppointmentNotification> notifications = new List<AppointmentNotification>();

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
                            notifications.Add(new AppointmentNotification(
                                (int)reader["Id"],
                                (DateTime)reader["DeliveryDateTime"],
                                reader["Message"].ToString(),
                                (int)reader["AppointmentId"]
                            ));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving appointment notifications", e);
            }

            return notifications;
        }
    }
}
