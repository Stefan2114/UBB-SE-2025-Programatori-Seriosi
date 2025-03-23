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
        private readonly Config _config;
        private Task<List<TreatmentDrug>> _treatmentdrugs;

        private TreatmentDrugModel()
        {
            _config = Config.Instance;
        }

        public static TreatmentDrugModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TreatmentDrugModel();
                }
                return _instance;
            }
        }

        public List<TreatmentDrug> getTreatmentDrugs(int mrId)
        {
            const string query = "SELECT * FROM TreatmentDrugs WHERE TreatmentId = @mrID";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@mrId", mrId);

                        List<TreatmentDrug> TreatmentDrugList = new List<TreatmentDrug>();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TreatmentDrug treatmentdrug = new TreatmentDrug();
                                treatmentdrug.Id = reader.GetInt32(0);
                                treatmentdrug.TreatmentId = reader.GetInt32(1);
                                treatmentdrug.DrugId = reader.GetInt32(2);
                                treatmentdrug.Quantity = reader.GetDouble(3);
                                treatmentdrug.StartTime = TimeOnly.FromTimeSpan(reader.GetFieldValue<TimeSpan>(4));
                                treatmentdrug.StartDate = DateOnly.FromDateTime(reader.GetFieldValue<DateTime>(5));
                                treatmentdrug.NrDays = reader.GetInt32(6);
                                TreatmentDrugList.Add(treatmentdrug);
                            }
                            return TreatmentDrugList;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting treatmentdrugs", e);
            }

        }
    }
}
