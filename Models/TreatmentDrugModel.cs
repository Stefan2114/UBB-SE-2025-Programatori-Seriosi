﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;

namespace Team3.Models
{
    public class TreatmentDrugModel
    {
        private static TreatmentDrugModel? _instance;
        private static readonly object _lock = new object();
        private readonly Config _config;

        private TreatmentDrugModel()
        {
            _config = Config.Instance;
        }

        public static TreatmentDrugModel Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new TreatmentDrugModel();
                    }
                }
                return _instance;
            }
        }

        public void addTreatmentDrug(TreatmentDrug treatmentDrug)
        {
            const string query = "INSERT INTO treatments_drugs(id,treatment_id,drug_id,quantity,starttime,endtime,startdate,nrdays) VALUES (@id,@treatment_id,@drug_id,@quantity,@starttime,@endtime,@startdate,@nrdays)";
            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", treatmentDrug.Id);
                command.Parameters.AddWithValue("@treatment_id", treatmentDrug.TreatmentId);
                command.Parameters.AddWithValue("@drug_id", treatmentDrug.DrugId);
                command.Parameters.AddWithValue("@quantity", treatmentDrug.Quantity);
                command.Parameters.AddWithValue("@starttime", treatmentDrug.StartTime);
                command.Parameters.AddWithValue("@endtime", treatmentDrug.EndTime);
                command.Parameters.AddWithValue("@startdate", treatmentDrug.StartDate);
                command.Parameters.AddWithValue("@nrdays", treatmentDrug.NrDays);

                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error adding treatmentdrug", e);
            }
        }

        public List<TreatmentDrug> getTreatmentDrugs(int treatmentId)
        {
            const string query = "SELECT * FROM treatments_drugs WHERE treatment_id = @treatment_id";

            try
            {
                SqlConnection connection = new SqlConnection(Config.CONNECTION);

                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@treatment_id", treatmentId);

                List<TreatmentDrug> TreatmentDrugList = new List<TreatmentDrug>();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TreatmentDrugList.Add(new TreatmentDrug(reader.GetInt32(0), reader.GetInt32(1),
                        reader.GetInt32(2), Convert.ToDouble((decimal)reader[3]), 
                        TimeOnly.FromTimeSpan(reader.GetFieldValue<TimeSpan>(4)),
                        TimeOnly.FromTimeSpan(reader.GetFieldValue<TimeSpan>(5)),
                        DateOnly.FromDateTime(reader.GetFieldValue<DateTime>(6)),
                        reader.GetInt32(7)));
                }

                connection.Close();
                return TreatmentDrugList;

            }
            catch (Exception e)
            {
                throw new Exception("Error getting treatmentdrugs", e);
            }

        }


    }
}
