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
    }
}
