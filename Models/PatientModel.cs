using System;
using System.Data.SqlClient;
using Team3.Entities;

namespace Team3.Models
{
    public class PatientModel
    {
        private static PatientModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;
        private PatientModel() {
            _config = Config.Instance;
        }

        public static PatientModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new PatientModel();
                        }
                    }
                }
                return _instance;
            }
        }

        public Patient GetPatient(int id)
        {
            const string query = "SELECT id, name, address FROM Patients WHERE id = @Id";

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
                                return new Patient(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("name")),
                                    reader.GetString(reader.GetOrdinal("address"))
                                );
                            }
                        }
                    }
                }

                throw new Exception("Patient not found");
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving patient", e);
            }
        }
    }
}
