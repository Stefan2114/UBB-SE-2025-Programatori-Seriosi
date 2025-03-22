using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Domain;

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

        public List<MedicalRecord> GetMedicalRecords()
        {
            const string query = "SELECT MedicalRecordId, DoctorId FROM MedicalRecord;";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    List<MedicalRecord> records = new List<MedicalRecord>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(new MedicalRecord(
                                reader.GetInt32(0),
                                reader.GetInt32(1)
                            ));
                        }
                    }
                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting medical records", e);
            }
        }
    }
}
