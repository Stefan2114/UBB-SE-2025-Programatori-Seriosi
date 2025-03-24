using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Entities;

namespace Team3.Models
{
    public class HospitalizationModel
    {
        private static HospitalizationModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;

        private HospitalizationModel()
        {
            _config = Config.Instance;
        }

        public static HospitalizationModel Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new HospitalizationModel();
                    }
                }
                return _instance;
            }
        }

        public List<Hospitalization> GetHospitalizations()
        {
            const string query = "SELECT * FROM Hospitalization;";

            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                List<Hospitalization> hospitalizationList = new List<Hospitalization>();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    hospitalizationList.Add(new Hospitalization(
                        reader.GetInt32(0),  // HospitalizationId
                        reader.GetInt32(1),  // RoomId
                        reader.GetDateTime(2),  // StartDate
                        reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)  // EndDate (nullable)
                    ));
                }

                connection.Close();
                return hospitalizationList;
            }
            catch (Exception e)
            {
                throw new Exception("Error getting hospitalizations", e);
            }
        }
    }
}
