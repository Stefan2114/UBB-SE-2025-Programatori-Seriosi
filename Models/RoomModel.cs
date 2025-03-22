using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Team3.Domain;

namespace Team3.Models
{
    public class RoomModel
    {
        private static RoomModel? _instance;
        private readonly Config _config;

        private RoomModel()
        {
            _config = Config.Instance;
        }

        public static RoomModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RoomModel();
                }
                return _instance;
            }
        }

        public List<Room> GetRooms()
        {
            const string query = "SELECT RoomId, RoomName, DepartmentId FROM Room;";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    List<Room> rooms = new List<Room>();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new Room(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetInt32(2)
                            ));
                        }
                    }
                    return rooms;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error getting rooms", e);
            }
        }
    }
}
