using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Domain;

namespace Team3.Models
{
    public class ScheduleModel
    {
        private static ScheduleModel? _instance;
        private readonly Config _config;

        private ScheduleModel()
        {
            _config = Config.Instance;
        }

        public static ScheduleModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScheduleModel();
                }
                return _instance;
            }
        }

        public List<Schedule> GetSchedules()
        {
            const string query = "SELECT ScheduleId, ScheduleWorkDay, DoctorId, ShiftTypeId FROM Schedule;";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    List<Schedule> schedules = new List<Schedule>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            schedules.Add(new Schedule(
                               reader.GetInt32(0),  // ScheduleId
                               DateOnly.FromDateTime(reader.GetDateTime(1)),  // Fixed: Convert to DateOnly
                               reader.GetInt32(2),  // DoctorId
                               reader.GetInt32(3)   // ShiftTypeId
                            ));
                        }
                    }
                    return schedules;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting schedules", e);
            }
        }
    }
}
