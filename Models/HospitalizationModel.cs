using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Entities;

namespace Team3.Models
{
    public class HospitalizationModel
    {
        private static HospitalizationModel? _instance;
        private readonly Config _config;

        private HospitalizationModel()
        {
            _config = Config.Instance;
        }

        public static HospitalizationModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HospitalizationModel();
                }
                return _instance;
            }
        }

        public List<Hospitalization> GetHospitalizations()
        {
            const string query = "SELECT HospitalizationId, RoomId, StartDate, EndDate FROM Hospitalization;";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    List<Hospitalization> hospitalizations = new List<Hospitalization>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hospitalizations.Add(new Hospitalization(
                                reader.GetInt32(0),
                                reader.GetInt32(1),
                                reader.GetDateTime(2),
                                reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3)
                            ));
                        }
                    }
                    return hospitalizations;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting hospitalizations", e);
            }
        }
    }
}
