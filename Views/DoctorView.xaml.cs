using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using Team3.DTOs;

namespace Team3.Views
{
    public sealed partial class DoctorView : Page
    {
        private List<DoctorDTO> _allDoctors; // Store the hardcoded data

        public DoctorView()
        {
            this.InitializeComponent();
            LoadHardcodedDoctors();  // Load hardcoded data
            LoadDoctors(DateTime.MinValue, DateTime.MaxValue);
        }

        private void LoadHardcodedDoctors()
        {
            _allDoctors = new List<DoctorDTO>
            {
                new DoctorDTO {  Name = "Bogdan", TotalHoursWorked = 40, PatientsTreated = 15 },
                new DoctorDTO {  Name = "Maria", TotalHoursWorked = 32, PatientsTreated = 10 },
                new DoctorDTO {  Name = "Stefan", TotalHoursWorked = 25, PatientsTreated = 7 },
                new DoctorDTO {  Name = "Paul", TotalHoursWorked = 50, PatientsTreated = 20 },
                new DoctorDTO {  Name = "VeryLongDoctorNameThatNeedsToBeTruncated", TotalHoursWorked = 10, PatientsTreated = 3 }
            };
        }

        private void LoadDoctors(DateTime startDate, DateTime endDate)
        {
            // Since the data is hardcoded, we return all doctors (date filtering can be added if needed)
            DoctorListView.ItemsSource = _allDoctors;
        }

        private void TimePeriodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime startDate;
            DateTime endDate = DateTime.Today;

            if (TimePeriodComboBox.SelectedIndex == 0) // Last 7 Days
            {
                startDate = DateTime.Today.AddDays(-7);
            }
            else if (TimePeriodComboBox.SelectedIndex == 1) // Last 30 Days
            {
                startDate = DateTime.Today.AddDays(-30);
            }
            else // All Time
            {
                startDate = DateTime.MinValue;
            }

            LoadDoctors(startDate, endDate);
        }
    }
}
