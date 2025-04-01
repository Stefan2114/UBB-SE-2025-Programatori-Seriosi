using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using Team3.DTOs;
using Team3.Models;
using Team3.Entities;

namespace Team3.Views
{
    public sealed partial class DoctorView : Page
    {
        private readonly DoctorModel _doctorModel;
        private readonly ScheduleModel _scheduleModel;
        private readonly AppointmentModel _appointmentModel;
        private readonly ShiftTypeModel _shiftTypeModel;
        private ListView doctorListView;

        public DoctorView()
        {
            this.InitializeComponent();
            _doctorModel = DoctorModel.Instance;
            _scheduleModel = ScheduleModel.Instance;
            _appointmentModel = AppointmentModel.Instance;
            _shiftTypeModel = ShiftTypeModel.Instance;
            doctorListView = this.FindName("DoctorListView") as ListView;

            LoadDoctors(DateTime.Today.AddDays(-7), DateTime.Today); // Default to last week
            TimePeriodComboBox.SelectedIndex = 0; // Select "Last Week" by default
        }

        private void LoadDoctors(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Step 1: Get doctors with names
                var doctorsWithNames = _doctorModel.GetAllDoctorsWithNames();
                var doctorDTOs = new List<DoctorDTO>();

                foreach (var (doctor, name) in doctorsWithNames)
                {
                    try
                    {
                        // Step 2: Get schedules for each doctor
                        List<Schedule> schedules;
                        try
                        {
                            schedules = _scheduleModel.GetDoctorSchedules(doctor.Id, startDate, endDate);
                        }
                        catch (Exception scheduleEx)
                        {
                            ShowError("Schedule Error", $"Error getting schedules for doctor {name}: {scheduleEx.Message}");
                            continue;
                        }

                        // Step 3: Calculate total hours
                        int totalHours;
                        try
                        {
                            totalHours = CalculateTotalHours(schedules);
                        }
                        catch (Exception hoursEx)
                        {
                            ShowError("Hours Calculation Error", $"Error calculating hours for doctor {name}: {hoursEx.Message}");
                            continue;
                        }

                        // Step 4: Get appointments
                        List<Appointment> appointments;
                        try
                        {
                            appointments = _appointmentModel.GetDoctorAppointments(doctor.Id, startDate, endDate);
                        }
                        catch (Exception appointmentEx)
                        {
                            ShowError("Appointment Error", $"Error getting appointments for doctor {name}: {appointmentEx.Message}");
                            continue;
                        }

                        // Step 5: Create DTO
                        string displayName = name.Length > 128 ? name.Substring(0, 128) + "..." : name;
                        doctorDTOs.Add(new DoctorDTO(
                            displayName,
                            totalHours,
                            appointments.Count
                        ));
                    }
                    catch (Exception doctorEx)
                    {
                        ShowError("Doctor Processing Error", $"Error processing doctor {name}: {doctorEx.Message}");
                    }
                }

                // Step 6: Update ListView
                if (doctorListView != null)
                {
                    doctorListView.ItemsSource = doctorDTOs;
                }
                else
                {
                    ShowError("UI Error", "ListView not found");
                }
            }
            catch (Exception ex)
            {
                ShowError("General Error", $"Failed to load doctor statistics: {ex.Message}\nStack trace: {ex.StackTrace}");
            }
        }

        private int CalculateTotalHours(List<Schedule> schedules)
        {
            int totalHours = 0;
            foreach (var schedule in schedules)
            {
                try
                {
                    var shiftType = _shiftTypeModel.GetShiftType(schedule.ShiftTypeId);
                    if (shiftType != null)
                    {
                        var duration = shiftType.ShiftTypeEndTime - shiftType.ShiftTypeStartTime;
                        totalHours += duration.Hours;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error calculating hours for schedule {schedule.ScheduleId}: {ex.Message}");
                }
            }
            return totalHours;
        }

        private async void ShowError(string title, string message)
        {
            try
            {
                var dialog = new ContentDialog
                {
                    Title = title,
                    Content = message,
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                };
                await dialog.ShowAsync();
            }
            catch (Exception ex)
            {
                // Fallback to debug output if dialog fails
                System.Diagnostics.Debug.WriteLine($"Error showing dialog: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Original error - {title}: {message}");
            }
        }

        private void TimePeriodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TimePeriodComboBox.SelectedIndex == -1) return;

            DateTime startDate;
            DateTime endDate = DateTime.Today;

            switch (TimePeriodComboBox.SelectedIndex)
            {
                case 0: // Last Week
                    startDate = DateTime.Today.AddDays(-7);
                    break;
                case 1: // Last Month
                    startDate = DateTime.Today.AddMonths(-1);
                    break;
                case 2: // Last 6 Months
                    startDate = DateTime.Today.AddMonths(-6);
                    break;
                case 3: // Last Year
                    startDate = DateTime.Today.AddYears(-1);
                    break;
                default:
                    startDate = DateTime.Today.AddDays(-7);
                    break;
            }

            LoadDoctors(startDate, endDate);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AuditOptionPage));
        }
    }
}
