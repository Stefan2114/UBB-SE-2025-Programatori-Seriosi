using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;

namespace Team3.Models
{
    public class AppointmentModel
    {
        private DbConnection dbConnection;

        public AppointmentModel(DbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public void AddAppointment(Appointment appointment)
        {
            dbConnection.Open();
            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = "INSERT INTO Appointments (id ,doctorId, patientId, appointmentDate, location) VALUES (@Id,@DoctorId, @PatientId, @AppointmentDate, @Location)";
            // Add parameters
            DbParameter idParameter = dbCommand.CreateParameter();
            idParameter.ParameterName = "@Id";
            idParameter.Value = appointment.id;
            DbParameter doctorIdParameter = dbCommand.CreateParameter();
            doctorIdParameter.ParameterName = "@DoctorId";
            doctorIdParameter.Value = appointment.doctorId;
            DbParameter patientIdParameter = dbCommand.CreateParameter();
            patientIdParameter.ParameterName = "@PatientId";
            patientIdParameter.Value = appointment.patientId;
            DbParameter appointmentDateParameter = dbCommand.CreateParameter();
            appointmentDateParameter.ParameterName = "@AppointmentDate";
            appointmentDateParameter.Value = appointment.appointmentDate;
            DbParameter locationParameter = dbCommand.CreateParameter();
            locationParameter.ParameterName = "@Location";
            locationParameter.Value = appointment.location;
            dbCommand.Parameters.Add(idParameter);
            dbCommand.Parameters.Add(doctorIdParameter);
            dbCommand.Parameters.Add(patientIdParameter);
            dbCommand.Parameters.Add(appointmentDateParameter);
            dbCommand.Parameters.Add(locationParameter);
            dbCommand.ExecuteNonQuery();
            dbConnection.Close();
    }
    public Appointment getAppointment(int id)
        {
        dbConnection.Open();
        DbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "SELECT * FROM Appointments WHERE id = @Id";
        DbParameter idParameter = dbCommand.CreateParameter();
        idParameter.ParameterName = "@Id";
        idParameter.Value = id;
        dbCommand.Parameters.Add(idParameter);
        DbDataReader dbDataReader = dbCommand.ExecuteReader();
        dbDataReader.Read();
        Appointment appointment = new Appointment();
        appointment.id = dbDataReader.GetInt32(0);
        appointment.doctorId = dbDataReader.GetInt32(1);
        appointment.patientId = dbDataReader.GetInt32(2);
        appointment.appointmentDate = dbDataReader.GetDateTime(3);
        appointment.location = dbDataReader.GetString(4);
        dbConnection.Close();
        return appointment;
        }

    }
}
