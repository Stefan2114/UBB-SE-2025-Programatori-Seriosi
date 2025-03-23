using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;


namespace Team3.Models
{
    public class DrugModel
    {
        private static DrugModel? _instance;
        private readonly Config _config;
        private Task<List<Drug>> _drugs;

        private DrugModel()
        {
            _config = Config.Instance;
        }

        public static DrugModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DrugModel();
                }
                return _instance;
            }
        }

        public Drug getReview(int mrId)
        {
            const string query = "SELECT * FROM Drugs WHERE id = @mrId;";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@mrId", mrId);

                        Drug drug = new Drug();

                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();

                        drug.Id = reader.GetInt32(0);
                        drug.Name = reader.GetString(1);
                        drug.Administration= reader.GetString(2);
                        return drug;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting drug", e);
            }

        }
    }
}
