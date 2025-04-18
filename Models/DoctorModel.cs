
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
            const string query = "SELECT * FROM doctors WHERE id = @id";

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
                            if (reader.Read())
                            {
                                return new Doctor((int)reader[0], (int)reader[1]);
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
