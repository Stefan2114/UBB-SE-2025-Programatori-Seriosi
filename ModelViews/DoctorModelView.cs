using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Team3.Entities;
using Team3.Models;

namespace Team3.ModelViews
{
    public class DoctorModelView
    {
        // Attributes
        public ObservableCollection<Doctor> DoctorsInfo { get; private set; }
        private readonly DoctorModel _doctorModel;
        public Action? OnBackNavigation { get; set; } // Delegate for back navigation handling

        // Constructor
        public DoctorModelView()
        {
            _doctorModel = new DoctorModel();
            DoctorsInfo = new ObservableCollection<Doctor>();
            LoadDoctorsInfo();
        }

        // Get all doctors
        public List<Doctor> GetDoctors()
        {
            try
            {
                return _doctorModel.getDoctors();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving doctors: {ex.Message}");
                return new List<Doctor>();
            }
        }

        // Get doctors by department ID
        public List<Doctor> GetDoctorsByDepartmentID(int departmentId)
        {
            try
            {
                var doctorList = _doctorModel.getDoctors();
                var filteredDoctors = doctorList.FindAll(d => d.DepartmentId == departmentId);

                if (filteredDoctors.Count == 0)
                {
                    Debug.WriteLine($"No doctors found for Department ID {departmentId}.");
                }

                return filteredDoctors;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error filtering doctors by Department ID: {ex.Message}");
                throw;
            }
        }

        // Handles date selection in ComboBox
        public void DateSelectedComboBoxHandler(DateOnly selectedDate)
        {
            try
            {
                Debug.WriteLine($"Date selected: {selectedDate}");
                LoadDoctorsInfo();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handling date selection: {ex.Message}");
            }
        }

        // Load all doctors
        public void LoadDoctorsInfo()
        {
            try
            {
                var doctorList = _doctorModel.getDoctors();
                if (doctorList != null && doctorList.Count > 0)
                {
                    DoctorsInfo.Clear();
                    foreach (var doctor in doctorList)
                    {
                        DoctorsInfo.Add(doctor);
                        Debug.WriteLine($"Loaded Doctor: ID = {doctor.DoctorId}, Name = {doctor.Name}");
                    }
                }
                else
                {
                    Debug.WriteLine("No doctors found in the database.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading doctors: {ex.Message}");
            }
        }

        // Get a specific doctor by ID
        public Doctor? GetDoctor(int doctorId)
        {
            try
            {
                return _doctorModel.getDoctors().FirstOrDefault(d => d.DoctorId == doctorId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving doctor by ID: {ex.Message}");
                return null;
            }
        }

        // Handles the Back Button press
        public void BackButtonHandler()
        {
            try
            {
                Debug.WriteLine("Back button pressed. Navigating to the previous screen...");
                OnBackNavigation?.Invoke(); // Calls the delegate if assigned
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error handling back button: {ex.Message}");
            }
        }
    }
}
