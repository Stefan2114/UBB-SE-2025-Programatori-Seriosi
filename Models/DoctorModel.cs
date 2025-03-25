using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;

namespace Team3.Models
{
    public class DoctorModel
    {
        private static DoctorModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;

        private DoctorModel() {
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

        public Doctor GetDoctor(int id)
        {
            const string query = "SELECT id, name, specialty FROM Doctors WHERE id = @Id";

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
                            if (reader.Read())
                            {
                                return new Doctor(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("name")),
                                    reader.GetString(reader.GetOrdinal("specialty"))
                                );
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
    }
}
