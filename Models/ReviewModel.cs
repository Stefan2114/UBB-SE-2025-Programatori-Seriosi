using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Team3.Entities;

namespace Team3.Models
{
    public class ReviewModel
    {
        private static ReviewModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;

        private ReviewModel()
        {
            _config = Config.Instance;
        }

        public static ReviewModel Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ReviewModel();
                    }
                }
                return _instance;
            }
        }
        
        public void addReview(Review review)
        {
            const string query = "INSERT INTO Reviews (id, MedicalRecordId, message, nrStars) VALUES (@Id, @MedicalRecordId, @Message, @NrStars)";

            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Id", review.Id);
                command.Parameters.AddWithValue("@MedicalRecordId", review.medicalRecordId);
                command.Parameters.AddWithValue("@Message", review.Message);
                command.Parameters.AddWithValue("@NrStars", review.NrStars);

                command.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception e)
            {
                throw new Exception("Error adding review", e);
            }
        }

        public Review getReview(int mrId)
        {
            const string query = "SELECT * FROM reviews WHERE id = @mrId;";

            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@mrId", mrId);

                Review review = new Review();

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                review.Id = reader.GetInt32(0);
                review.medicalRecordId = reader.GetInt32(1);
                review.Message = reader.GetString(2);
                review.NrStars = reader.GetInt32(3);

                connection.Close();
                return review;

            }
            catch (Exception e)
            {
                throw new Exception("Error getting review", e);
            }

        }

    }
}
