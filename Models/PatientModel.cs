using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;

namespace Team3.Models
{
    public class PatientModel
    {
        private DbConnection dbConnection;
        
        public PatientModel(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public Patient getPatient(int id)
        {
            dbConnection.Open();
            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "SELECT * FROM Patients WHERE id = @Id";
            DbParameter idParameter = dbCommand.CreateParameter();
            idParameter.ParameterName = "@Id";
            idParameter.Value = id;
            dbCommand.Parameters.Add(idParameter);
            DbDataReader dbDataReader = dbCommand.ExecuteReader();
            dbDataReader.Read();
            Patient patient = new Patient(dbDataReader.GetInt32(0), dbDataReader.GetString(1), dbDataReader.GetString(2));
            dbConnection.Close();
            return patient;
        }
    }
}
