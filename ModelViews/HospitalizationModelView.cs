using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Team3.Models;
using Team3.Entities;


namespace Team3.ModelViews
{
    public class HospitalizationModelView
    {
        // Attributes
        public ObservableCollection<Hospitalization> HospitalizationsInfo { get; private set; }
        private readonly HospitalizationModel _hospitalizationModel;

        // Constructor
        public HospitalizationModelView()
        {
            _hospitalizationModel = new HospitalizationModel();
            HospitalizationsInfo = new ObservableCollection<Hospitalization>();
            LoadHospitalizationsInfo();
        }

        // Load detailed hospitalization information
        public void LoadHospitalizationsInfo()
        {
            try
            {
                var hospitalizationList = _hospitalizationModel.getHospitalizations();

                if (hospitalizationList != null && hospitalizationList.Count > 0)
                {
                    HospitalizationsInfo.Clear(); // Clear old data
                    foreach (var hospitalization in hospitalizationList)
                    {
                        HospitalizationsInfo.Add(hospitalization);
                        Debug.WriteLine($"Loaded Hospitalization: ID = {hospitalization.HospitalizationId}, Patient ID = {hospitalization.PatientId}");
                    }
                }
                else
                {
                    Debug.WriteLine("No hospitalizations found in the database.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading hospitalization details: {ex.Message}");
            }
        }

        // Get hospitalizations by room ID
        public List<Hospitalization> GetHospitalizationsByRoomID(int roomId)
        {
            try
            {
                var hospitalizationList = _hospitalizationModel.getHospitalizations();
                var filteredHospitalizations = hospitalizationList.FindAll(h => h.RoomId == roomId);

                if (filteredHospitalizations.Count == 0)
                {
                    Debug.WriteLine($"No hospitalizations found for Room ID {roomId}.");
                }

                return filteredHospitalizations;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error filtering hospitalizations: {ex.Message}");
                throw;
            }
        }
    }
}
