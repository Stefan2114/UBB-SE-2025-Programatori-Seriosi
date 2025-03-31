﻿using System;
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
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new EquipmentModel();
                        }
                    }
                }
                return _instance;
            }
        }

        
        // Get all equipment
        public List<Equipment> GetEquipments()
        {
            const string query = "SELECT EquipmentId, Name, Description FROM Equipment";

            try
            {
                List<Equipment> equipmentList = new List<Equipment>();

                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                equipmentList.Add(new Equipment(
                                    reader.GetInt32(reader.GetOrdinal("EquipmentId")),
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetString(reader.GetOrdinal("Description"))
                                ));
                            }
                        }
                    }
                }

                return equipmentList;
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving equipments", e);
            }
        }
    }
}
