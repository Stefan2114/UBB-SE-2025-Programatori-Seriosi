using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Entities;

namespace Team3.Models
{
    public class DoctorModel
    {
        private static DoctorModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;

        private DoctorModel()
        {
            _config = Config.Instance;
        }

        public static DoctorModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DoctorModel();
                        }
                    }
                }
                return _instance;
            }
        }

        // Get a single doctor by their userId
        public Doctor GetDoctor(int userId)
        {
            const string query = "SELECT user_id FROM Doctors WHERE user_id = @UserId";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Doctor(reader.GetInt32(reader.GetOrdinal("user_id")));
                            }
                        }
                    }
                }

                throw new Exception("Doctor not found");
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving doctor", e);
            }
        }

        // Get all doctors
        public List<Doctor> GetDoctors()
        {
            const string query = "SELECT user_id FROM Doctors";

            try
            {
                List<Doctor> doctors = new List<Doctor>();

                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                doctors.Add(new Doctor(reader.GetInt32(reader.GetOrdinal("user_id"))));
                            }
                        }
                    }
                }

                return doctors;
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving doctors", e);
            }
        }
    }
}
