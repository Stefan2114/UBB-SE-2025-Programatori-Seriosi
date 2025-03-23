using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;

namespace Team3.Models
{
    public class TreatmentModel
    {
        private static TreatmentModel? _instance;
        private readonly Config _config;
        private Task<List<Treatment>> _treatments;

        private TreatmentModel()
        {
            _config = Config.Instance;
        }

        public static TreatmentModel Instance
        {
            get
            {   if(_instance == null)
                {
                    _instance = new TreatmentModel();
                }
            return _instance;
            }
        }

        public void addTreatment(Treatment treatment)
        {
            const string query = "INSERT INTO Treatments(id, MedicalRecordid) values (@Id , @MedicalRecordId)";
            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION)) {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", treatment.Id);
                        command.Parameters.AddWithValue("@MedicalRecordId", treatment.MedicalRecordId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error adding treatment", e);
            }
        }

    }
}
