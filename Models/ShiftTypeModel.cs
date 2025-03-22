using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using Team3.Domain;

namespace Team3.Models
{
    public class ShiftTypeModel
    {
        private static ShiftTypeModel? _instance;
        private readonly Config _config;

        private ShiftTypeModel()
        {
            _config = Config.Instance;
        }

        public static ShiftTypeModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ShiftTypeModel();
                }
                return _instance;
            }
        }

        public List<ShiftType> GetShiftTypes()
        {
            const string query = "SELECT ShiftTypeId, ShiftTypeStartTime, ShiftTypeEndTime FROM ShiftType;";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    List<ShiftType> shiftTypes = new List<ShiftType>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shiftTypes.Add(new ShiftType(
                                reader.GetInt32(0),
                                reader.GetDateTime(1),
                                reader.GetDateTime(2)
                            ));
                        }
                    }
                    return shiftTypes;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting shift types", e);
            }
        }
    }
}
