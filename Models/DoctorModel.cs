using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Domain;

namespace Team3.Models
{
    public class DoctorModel
    {
        private static DoctorModel? _instance;
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
                    _instance = new DoctorModel();
                }
                return _instance;
            }
        }

        public List<Doctor> GetDoctors()
        {
            const string query = "SELECT DoctorId, DoctorName FROM Doctor;";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    List<Doctor> doctors = new List<Doctor>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctors.Add(new Doctor(
                                reader.GetInt32(0),
                                reader.GetString(1)
                            ));
                        }
                    }
                    return doctors;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting doctors", e);
            }
        }
    }
}
