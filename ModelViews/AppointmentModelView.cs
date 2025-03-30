using System;
using Team3.Entities;
using Team3.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace Team3.ViewModels
{
    public class AppointmentModelView : INotifyPropertyChanged
    {
        // Holds a reference to the singleton AppointmentModel instance to interact with the database.
        private readonly AppointmentModel _appointmentModel;

        // Stores the currently selected or added appointment.
        private Appointment? _currentAppointment;

        // Event triggered when a property value changes, allowing UI updates.
        public event PropertyChangedEventHandler? PropertyChanged;

        public AppointmentModelView()
        {
            _appointmentModel = AppointmentModel.Instance;

            // Command for handling the Add Appointment button click event.
            AddAppointmentCommand = new RelayCommand(AddAppointmentButtonHandler);
        }

        public ICommand AddAppointmentCommand { get; }

        // Property that holds the current appointment and notifies the UI when updated.
        public Appointment? CurrentAppointment
        {
            get => _currentAppointment;
            set
            {
                _currentAppointment = value;
                OnPropertyChanged("CurrentAppointment");
            }
        }

        // Adds a new appointment to the database and updates the CurrentAppointment property.
        public void AddAppointment(Appointment appointment)
        {
            try
            {
                _appointmentModel.AddAppointment(appointment);
                CurrentAppointment = appointment;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding appointment: {ex.Message}");
            }
        }

        // Retrieves an appointment from the database by ID and updates the CurrentAppointment property.
        public Appointment? GetAppointment(int id)
        {
            try
            {
                CurrentAppointment = _appointmentModel.GetAppointment(id);
                return CurrentAppointment;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving appointment: {ex.Message}");
                return null;
            }
        }

        // Handles button click events, ensuring the provided parameter is an Appointment before adding it.
        private void AddAppointmentButtonHandler(object? parameter)
        {
            if (parameter is Appointment appointment)
            {
                AddAppointment(appointment);
            }
        }

        // Notifies subscribers that a property has changed, allowing UI elements to update accordingly.
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
