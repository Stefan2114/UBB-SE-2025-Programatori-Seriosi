using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Documents;
using Team3.Entities;

namespace Team3.Models
{
    public class TreatmentModel
    {
        private static TreatmentModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;

        private TreatmentModel()
        {
            _config = Config.Instance;
        }

        public static TreatmentModel Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new TreatmentModel();
                    }
                }
                return _instance;
            }
        }

        public void addTreatment(Treatment treatment)
        {
            const string query = "INSERT INTO treatments(id, Memdicalrecord_id) values (@id , @Memdicalrecord_id)";
            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", treatment.Id);
                command.Parameters.AddWithValue("@Memdicalrecord_id", treatment.MedicalRecordId);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error adding treatment", e);
            }
        }

    }
}
