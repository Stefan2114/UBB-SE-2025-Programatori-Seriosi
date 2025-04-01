using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Entities;

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
            const string query = "SELECT Id, DoctorId, WorkDay, ShiftTypeId FROM Schedule;";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine("Attempting to connect to database for GetSchedules...");
                        connection.Open();
                        System.Diagnostics.Debug.WriteLine("Database connection successful");

                        SqlCommand command = new SqlCommand(query, connection);
                        System.Diagnostics.Debug.WriteLine($"Executing query: {query}");
                        
                        List<Schedule> schedules = new List<Schedule>();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var schedule = new Schedule(
                                   reader.GetInt32(reader.GetOrdinal("Id")),
                                   DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("WorkDay"))),
                                   reader.GetInt32(reader.GetOrdinal("DoctorId")),
                                   reader.GetInt32(reader.GetOrdinal("ShiftTypeId"))
                                );
                                System.Diagnostics.Debug.WriteLine($"Loaded schedule: {schedule}");
                                schedules.Add(schedule);
                            }
                        }
                        System.Diagnostics.Debug.WriteLine($"Successfully loaded {schedules.Count} schedules");
                        return schedules;
                    }
                    catch (SqlException sqlEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"SQL Error in GetSchedules: {sqlEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"SQL State: {sqlEx.State}");
                        System.Diagnostics.Debug.WriteLine($"SQL Error Number: {sqlEx.Number}");
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetSchedules: {e.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {e.StackTrace}");
                throw new Exception("Error getting schedules", e);
            }
        }

        public List<Schedule> GetDoctorSchedules(int doctorId, DateTime startDate, DateTime endDate)
        {
            const string query = @"SELECT Id, DoctorId, WorkDay, ShiftTypeId 
                                FROM Schedule 
                                WHERE DoctorId = @doctorId 
                                AND WorkDay BETWEEN @startDate AND @endDate;";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine($"Attempting to get schedules for doctor {doctorId}");
                        System.Diagnostics.Debug.WriteLine($"Date range: {startDate} to {endDate}");
                        System.Diagnostics.Debug.WriteLine($"Query: {query}");
                        
                        connection.Open();
                        System.Diagnostics.Debug.WriteLine("Database connection successful");

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@doctorId", doctorId);
                            command.Parameters.AddWithValue("@startDate", startDate.Date);
                            command.Parameters.AddWithValue("@endDate", endDate.Date);

                            List<Schedule> schedules = new List<Schedule>();

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    try
                                    {
                                        var id = reader.GetInt32(reader.GetOrdinal("Id"));
                                        var workDay = reader.GetDateTime(reader.GetOrdinal("WorkDay"));
                                        var docId = reader.GetInt32(reader.GetOrdinal("DoctorId"));
                                        var shiftId = reader.GetInt32(reader.GetOrdinal("ShiftTypeId"));
                                        
                                        System.Diagnostics.Debug.WriteLine($"Read values - Id: {id}, DoctorId: {docId}, WorkDay: {workDay}, ShiftTypeId: {shiftId}");
                                        
                                        var schedule = new Schedule(
                                            id,
                                            DateOnly.FromDateTime(workDay),
                                            docId,
                                            shiftId
                                        );
                                        System.Diagnostics.Debug.WriteLine($"Created schedule object: {schedule}");
                                        schedules.Add(schedule);
                                    }
                                    catch (Exception readEx)
                                    {
                                        System.Diagnostics.Debug.WriteLine($"Error reading schedule row: {readEx.Message}");
                                        throw;
                                    }
                                }
                            }
                            System.Diagnostics.Debug.WriteLine($"Found {schedules.Count} schedules for doctor {doctorId}");
                            return schedules;
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"SQL Error in GetDoctorSchedules: {sqlEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"SQL State: {sqlEx.State}");
                        System.Diagnostics.Debug.WriteLine($"SQL Error Number: {sqlEx.Number}");
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetDoctorSchedules: {e.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {e.StackTrace}");
                throw new Exception("Error getting doctor schedules", e);
            }
        }
    }
}
