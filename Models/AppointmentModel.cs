﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Entities;

namespace Team3.Models
{
    public class AppointmentModel
    {
        private static AppointmentModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;

        private AppointmentModel() {
            _config = Config.Instance;
        }

        public static AppointmentModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppointmentModel();
                        }
                    }
                }
                return _instance;
            }
        }

        public void AddAppointment(Appointment appointment)
        {
            const string query = "INSERT INTO appointments (id, doctor_id, patient_id, appointment_datetime, location) " +
                                 "VALUES (@id, @doctor_id, @patient_id, @appointment_datetime, @location)";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", appointment.Id);
                        command.Parameters.AddWithValue("@doctor_id", appointment.DoctorId);
                        command.Parameters.AddWithValue("@patient_id", appointment.PatientId);
                        command.Parameters.AddWithValue("@appointment_datetime", appointment.AppointmentDateTime);
                        command.Parameters.AddWithValue("@location", appointment.Location);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error adding appointment", e);
            }
        }

        public List<Appointment> GetDoctorAppointments(int doctorId, DateTime startDate, DateTime endDate)
        {
            const string query = @"SELECT id, doctor_id, patient_id, appointment_datetime, location 
                                FROM appointments 
                                WHERE doctor_id = @doctor_id 
                                AND appointment_datetime BETWEEN @start_date AND @end_date";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@doctor_id", doctorId);
                        command.Parameters.AddWithValue("@start_date", startDate);
                        command.Parameters.AddWithValue("@end_date", endDate);

                        List<Appointment> appointments = new List<Appointment>();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                appointments.Add(new Appointment(
                                    (int)reader["id"],
                                    (int)reader["doctor_id"],
                                    (int)reader["patient_id"],
                                    (DateTime)reader["appointment_datetime"],
                                    reader["location"].ToString() ?? ""
                                ));
                            }
                        }
                        return appointments;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting doctor appointments", e);
            }
        }

        public Appointment GetAppointment(int id)
        {
            const string query = "SELECT id, doctor_id, patient_id, appointment_datetime, location FROM Appointments WHERE id = @id";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            return new Appointment((int)reader[0], 
                                (int)reader[1],
                                (int)reader[2],
                                (DateTime)reader[3], 
                                reader[4].ToString());
  
                        }
                    }
                }

                throw new Exception("Appointment not found");
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving appointment", e);
            }
        }
    }
}
