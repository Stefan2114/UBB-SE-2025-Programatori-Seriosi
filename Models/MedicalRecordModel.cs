using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Entities;

namespace Team3.Models
{
    public class MedicalRecordModel
    {
        private static MedicalRecordModel? _instance;
        private readonly Config _config;

        private MedicalRecordModel()
        {
            _config = Config.Instance;
        }

        public static MedicalRecordModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MedicalRecordModel();
                }
                return _instance;
            }
        }

        public void AddMedicalRecord(MedicalRecord medicalRecord)
        {
            const string query = "INSERT INTO MedicalRecords (Id, PatientId, DoctorId) VALUES (@Id, @PatientId, @DoctorId);";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", medicalRecord.Id);
                    command.Parameters.AddWithValue("@PatientId", medicalRecord.PatientId);
                    command.Parameters.AddWithValue("@DoctorId", medicalRecord.DoctorId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error adding medical record", e);
            }
        }

        public MedicalRecord GetMedicalRecord(int id)
        {
            const string query = "SELECT * FROM MedicalRecords WHERE Id = @Id;";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MedicalRecord((int)reader["Id"], (int)reader["PatientId"], (int)reader["DoctorId"]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving medical record", e);
            }

            return null;
        }
    }
}
