using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;
using Team3.DTOs;

namespace Team3.Models
{
    public class DoctorModel
    {
        private static DoctorModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;

        private DoctorModel() {
            _config = Config.Instance;
        }

        public static DoctorModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DoctorModel();
                        }
                    }
                }
                return _instance;
            }
        }

        public List<(Doctor Doctor, string Name)> GetAllDoctorsWithNames()
        {
            const string query = @"SELECT d.Id as DoctorId, d.UserId, d.DepartmentId, u.Username as Name 
                                FROM Doctor d 
                                JOIN Users u ON d.UserId = u.Id";
            var doctorsWithNames = new List<(Doctor Doctor, string Name)>();

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine("Attempting to connect to database...");
                        connection.Open();
                        System.Diagnostics.Debug.WriteLine("Database connection successful");

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            System.Diagnostics.Debug.WriteLine("Executing query: " + query);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var doctor = new Doctor(
                                        reader.GetInt32(reader.GetOrdinal("DoctorId")),
                                        reader.GetInt32(reader.GetOrdinal("UserId")),
                                        reader.GetInt32(reader.GetOrdinal("DepartmentId"))
                                    );
                                    var name = reader.GetString(reader.GetOrdinal("Name"));
                                    doctorsWithNames.Add((doctor, name));
                                }
                            }
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"SQL Error: {sqlEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"SQL State: {sqlEx.State}");
                        System.Diagnostics.Debug.WriteLine($"SQL Error Number: {sqlEx.Number}");
                        throw new Exception($"Database error: {sqlEx.Message}", sqlEx);
                    }
                }
                return doctorsWithNames;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetAllDoctorsWithNames: {e.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {e.StackTrace}");
                throw new Exception("Error retrieving all doctors: " + e.Message, e);
            }
        }

        public Doctor GetDoctor(int id)
        {
            const string query = "SELECT Id, UserId, DepartmentId FROM Doctor WHERE Id = @id";

            try
            {
                using (SqlConnection connection = new SqlConnection(Config.CONNECTION))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Doctor(
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetInt32(reader.GetOrdinal("UserId")),
                                    reader.GetInt32(reader.GetOrdinal("DepartmentId"))
                                );
                            }
                        }
                    }
                }

                throw new Exception("Doctor not found");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetDoctor: {e.Message}");
                throw new Exception("Error retrieving doctor: " + e.Message, e);
            }
        }
    }
}
