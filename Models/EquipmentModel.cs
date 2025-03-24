using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Entities;

namespace Team3.Models
{
    public class EquipmentModel
    {
        private static EquipmentModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;

        private EquipmentModel()
        {
            _config = Config.Instance;
        }

        public static EquipmentModel Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new EquipmentModel();
                    }
                }
                return _instance;
            }
        }

        public List<Equipment> GetEquipment()
        {
            const string query = "SELECT * FROM Equipment;";

            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);
            
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                List<Equipment> equipmentList = new List<Equipment>();

                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    equipmentList.Add(new Equipment(
                             reader.GetInt32(0),
                             reader.GetString(1),
                             reader.GetString(2)
                    ));
                }

                connection.Close();
                return equipmentList;
             
            }
            catch (Exception e)
            {
                throw new Exception("Error getting equipment", e);
            }
        }
    }
}
