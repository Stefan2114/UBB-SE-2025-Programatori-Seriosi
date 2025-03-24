using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;

namespace Team3.Models
{
    public class TreatmentDrugModel
    {
        private static TreatmentDrugModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;

        private TreatmentDrugModel()
        {
            _config = Config.Instance;
        }

        public static TreatmentDrugModel Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new TreatmentDrugModel();
                    }
                }
                return _instance;
            }
        }

        public List<TreatmentDrug> getTreatmentDrugs(int mrId)
        {
            const string query = "SELECT * FROM TreatmentDrugs WHERE TreatmentId = @mrID";

            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@mrId", mrId);

                List<TreatmentDrug> TreatmentDrugList = new List<TreatmentDrug>();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TreatmentDrug treatmentdrug = new TreatmentDrug();
                    treatmentdrug.Id = reader.GetInt32(0);
                    treatmentdrug.TreatmentId = reader.GetInt32(1);
                    treatmentdrug.DrugId = reader.GetInt32(2);
                    treatmentdrug.Quantity = reader.GetDouble(3);
                    treatmentdrug.StartTime = TimeOnly.FromTimeSpan(reader.GetFieldValue<TimeSpan>(4));
                    treatmentdrug.EndTime = TimeOnly.FromTimeSpan(reader.GetFieldValue<TimeSpan>(5));
                    treatmentdrug.StartDate = DateOnly.FromDateTime(reader.GetFieldValue<DateTime>(6));
                    treatmentdrug.NrDays = reader.GetInt32(7);
                    TreatmentDrugList.Add(treatmentdrug);
                }

                connection.Close();
                return TreatmentDrugList;

            }
            catch (Exception e)
            {
                throw new Exception("Error getting treatmentdrugs", e);
            }

        }
    }
}
