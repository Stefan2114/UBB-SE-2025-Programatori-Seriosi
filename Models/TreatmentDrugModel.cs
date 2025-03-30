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

        public void addTreatmentDrug(TreatmentDrug treatmentDrug)
        {
            const string query = "INSERT INTO TreatmentDrugs(Id,TreatmentId,DrugId,Quantity,StartTime,EndTime,StartDate,NrDays) VALUES (@Id,@TreatmentId,@DrugId,@Quantity,@StartTime,@EndTime,@StartDate,@NrDays)";
            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", treatmentDrug.Id);
                command.Parameters.AddWithValue("@TreatmentId", treatmentDrug.TreatmentId);
                command.Parameters.AddWithValue("@DrugId", treatmentDrug.DrugId);
                command.Parameters.AddWithValue("@Quantity", treatmentDrug.Quantity);
                command.Parameters.AddWithValue("@Quantity", treatmentDrug.Quantity);
                command.Parameters.AddWithValue("@StartTime", treatmentDrug.StartTime);
                command.Parameters.AddWithValue("@EndTime", treatmentDrug.EndTime);
                command.Parameters.AddWithValue("@StartDate", treatmentDrug.StartDate);
                command.Parameters.AddWithValue("@NrDays", treatmentDrug.NrDays);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error adding treatmentdrug", e);
            }
        }

        public List<TreatmentDrug> getTreatmentDrugs(int medicalrecordId)
        {
            const string query = "SELECT * FROM TreatmentDrugs WHERE medicalrecord_id = @medicalrecord_id";

            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@medicalrecord_id", medicalrecordId);

                List<TreatmentDrug> TreatmentDrugList = new List<TreatmentDrug>();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TreatmentDrugList.Add(new TreatmentDrug(reader.GetInt32(0), reader.GetInt32(1),
                        reader.GetInt32(2), reader.GetDouble(3), 
                        TimeOnly.FromTimeSpan(reader.GetFieldValue<TimeSpan>(4)),
                        TimeOnly.FromTimeSpan(reader.GetFieldValue<TimeSpan>(5)),
                        DateOnly.FromDateTime(reader.GetFieldValue<DateTime>(6)),
                        reader.GetInt32(7)));
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
