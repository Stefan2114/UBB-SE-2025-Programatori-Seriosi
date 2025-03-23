using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;

namespace Team3.Models
{
    public class DoctorModel
    {
        private DbConnection dbConnection;

        DoctorModel(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public Doctor getDoctor(int id)
        {
            dbConnection.Open();
            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "SELECT * FROM Doctors WHERE id = @Id";
            DbParameter idParameter = dbCommand.CreateParameter();
            idParameter.ParameterName = "@Id";
            idParameter.Value = id;
            dbCommand.Parameters.Add(idParameter);
            DbDataReader dbDataReader = dbCommand.ExecuteReader();
            dbDataReader.Read();
            Doctor doctor = new Doctor(dbDataReader.GetInt32(0), dbDataReader.GetString(1), dbDataReader.GetString(2));
            dbConnection.Close();
            return doctor;
        }
    }
}
