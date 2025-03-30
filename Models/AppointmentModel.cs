using System;
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
            const string query = "INSERT INTO Appointments (id, doctorId, patientId, appointmentDate, location) " +
                                 "VALUES (@Id, @DoctorId, @PatientId, @AppointmentDate, @Location)";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", appointment.id);
                        command.Parameters.AddWithValue("@DoctorId", appointment.doctorId);
                        command.Parameters.AddWithValue("@PatientId", appointment.patientId);
                        command.Parameters.AddWithValue("@AppointmentDate", appointment.appointmentDate);
                        command.Parameters.AddWithValue("@Location", appointment.location);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error adding appointment", e);
            }
        }

        public Appointment GetAppointment(int id)
        {
            const string query = "SELECT id, doctorId, patientId, appointmentDate, location FROM Appointments WHERE id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Check if data is available before accessing it
                            {
                                return new Appointment
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("id")),
                                    doctorId = reader.GetInt32(reader.GetOrdinal("doctorId")),
                                    patientId = reader.GetInt32(reader.GetOrdinal("patientId")),
                                    appointmentDate = reader.GetDateTime(reader.GetOrdinal("appointmentDate")),
                                    location = reader.GetString(reader.GetOrdinal("location"))
                                };
                            }
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
