using System;
using System.Collections.ObjectModel;
using Team3.Entities;
using Team3.Models;

namespace Team3.ModelViews
{
    public class DoctorModelView
    {
        // Observable collections for doctors' information
        public ObservableCollection<Doctor> DoctorsInfo { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }

        // Dependencies
        private readonly DoctorModel doctorModel;
        public MedicalRecordModelView MedicalRecordModelView { get; set; }
        public ScheduleViewModel ScheduleModelView { get; set; }
        public UserModelView UserModelView { get; set; }

        public DoctorModelView()
        {
            // Initialize the DoctorModel instance
            doctorModel = DoctorModel.Instance;

            // Initialize collections
            DoctorsInfo = new ObservableCollection<Doctor>();
            Doctors = new ObservableCollection<Doctor>();

            // Load doctors from database
            //LoadDoctors();
        }

        //private void LoadDoctors()
        //{
        //    try
        //    {
        //        var doctorsList = doctorModel.GetDoctors();

        //        // Clear existing data
        //        Doctors.Clear();
        //        DoctorsInfo.Clear();

        //        foreach (var doctor in doctorsList)
        //        {
        //            Doctors.Add(doctor);
        //            DoctorsInfo.Add(doctor);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error loading doctors: {ex.Message}");
        //    }
        //}
    }
}
