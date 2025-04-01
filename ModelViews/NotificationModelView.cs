using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Team3.Entities;
using Team3.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Team3.ModelViews
{


    public class NotificationModelView
    {

        private readonly NotificationModel _notificationModel;
        private readonly AppointmentModel _appointmentModel;
        private readonly DoctorModel _doctorModel;
        private readonly UserModelView _userModelView;
        private readonly PatientModel _patientModel;

        private readonly static string UPCOMING_APPOINTMENT_NOTIFICATION_TEMPLATE = "Tomorrow @datetime, you have an appointment with Dr. @doctor at location @location";
        private readonly static string APPOINTMENT_CANCEL_NOTIFICATION_TEMPLATE = "Patient: @patient has canceled their upcoming appointment, scheduled for @datetime at @location.";
        private readonly static string REVIEW_NOTIFICATION_TEMPLATE = "A review for doctor @doctor was added: @message; number of starts: @nrStarts";
        private readonly static string MEDICATION_REMINDER_NOTIFICATION_TEMPLATE = "It's time to take @drug, Quantity: @quantity, @administration";
        private readonly static string REVIEW_REMINDER_NOTIFICATION_TEMPLATE = "Reminder: Please leave a review for your last appointment.";

        private readonly static int HARDCODED_DOCTOR_ID = 1;
        private readonly static int HARDCODED_PATIENT_ID = 1;
        private readonly static int HARDCODED_APPOINTMENT_ID = 4;




        private string GetUpcomingAppointmentNotificationMessage(string datetime, string doctorName, string location)
        {
            string notificationMessage = UPCOMING_APPOINTMENT_NOTIFICATION_TEMPLATE;
            notificationMessage = notificationMessage.Replace("@datetime", datetime);
            notificationMessage = notificationMessage.Replace("@doctor", doctorName);
            notificationMessage = notificationMessage.Replace("@location", location);
            return notificationMessage;

        }


        private string GetAppointmentCancelNotificationMessage(string patientName, string datetime, string location)
        {
            string notificationMessage = APPOINTMENT_CANCEL_NOTIFICATION_TEMPLATE;
            notificationMessage = notificationMessage.Replace("@patient", patientName);
            notificationMessage = notificationMessage.Replace("@datetime", datetime);
            notificationMessage = notificationMessage.Replace("@location", location);
            return notificationMessage;
        }


        private string GetReviewNotificationMessage(string doctorName, string message, int @nrStarts)
        {
            string notificationMessage = REVIEW_NOTIFICATION_TEMPLATE;
            notificationMessage = notificationMessage.Replace("@doctor", doctorName);
            notificationMessage = notificationMessage.Replace("@message", message);
            notificationMessage = notificationMessage.Replace("@nrStarts", nrStarts.ToString());
            return notificationMessage;

        }



        private string GetMedicationReminderNotificationMessage(string drugName, double quantity, string administration)
        {
            string notificationMessage = MEDICATION_REMINDER_NOTIFICATION_TEMPLATE;
            notificationMessage = notificationMessage.Replace("@drug", drugName);
            notificationMessage = notificationMessage.Replace("@quantity", quantity.ToString());
            notificationMessage = notificationMessage.Replace("@administration", administration);
            return notificationMessage;

        }


        private string GetReviewReminderNotificationMessage(string drugName, double quantity, string administration)
        {
            string notificationMessage = REVIEW_REMINDER_NOTIFICATION_TEMPLATE;
            return notificationMessage;

        }

        public List<Notification> Notifications { get; private set; }

        public NotificationModelView()
        {
            _notificationModel = NotificationModel.Instance;
            _appointmentModel = AppointmentModel.Instance;
            _doctorModel = DoctorModel.Instance;
            _patientModel = PatientModel.Instance;
            _userModelView = new UserModelView();
            Notifications = new List<Notification>();
        }

        public void LoadNotifications(int userId)
        {

            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Config.ROMANIA_TIMEZONE);

            List<Notification> notifications = _notificationModel.GetUserNotifications(userId);
            Debug.WriteLine(notifications.ToString());
            notifications = notifications
                .Where(n => n.DeliveryDateTime < currentDateTime)
                .OrderByDescending(n => n.DeliveryDateTime)
                .ToList();
            foreach(Notification notification in notifications)
            {
                Notifications.Add(notification);
                Debug.WriteLine(notification.ToString());
            }
        }

        public void DeleteNotification(int userId)
        {
            _notificationModel.deleteNotification(userId);
        }

        public void AddAppointment(int userId)
        {
            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Config.ROMANIA_TIMEZONE);

            Appointment appointment = new Appointment(HARDCODED_APPOINTMENT_ID, HARDCODED_DOCTOR_ID, HARDCODED_PATIENT_ID, currentDateTime.AddDays(1), "FSEGA");

            _appointmentModel.AddAppointment(appointment);
            AddUpcomingAppointmentNotification(HARDCODED_APPOINTMENT_ID);

        }


        public void AddUpcomingAppointmentNotification(int appointmentId)
        {

            Appointment appointment = _appointmentModel.GetAppointment(appointmentId);
            Doctor doctor = _doctorModel.GetDoctor(appointment.DoctorId);
            User user = _userModelView.GetUser(doctor.UserId);

            Patient patient = _patientModel.GetPatient(appointment.PatientId);

            Debug.WriteLine(appointment.ToString());
            Debug.WriteLine(doctor.ToString());
            Debug.WriteLine(user.ToString());

            string upcomingAppointmentNotificationMessage = GetUpcomingAppointmentNotificationMessage(appointment.AppointmentDateTime.ToString(), user.Name, appointment.Location);
            int notificationId = _notificationModel.AddNotification(new Notification(patient.UserId,appointment.AppointmentDateTime.AddDays(-1), upcomingAppointmentNotificationMessage));

            Debug.WriteLine(appointment.ToString());
            Debug.WriteLine(doctor.ToString());
            Debug.WriteLine(user.ToString());
            Debug.WriteLine(notificationId.ToString());

            _notificationModel.AddAppointmentNotification(notificationId, appointmentId);
        }




        public void AddCancelAppointmentNotification(int appointmentId)
        {
            Appointment appointment = _appointmentModel.GetAppointment(appointmentId);
            Debug.WriteLine(appointment.ToString());
            Doctor doctor = _doctorModel.GetDoctor(appointment.DoctorId);

            Patient patient = _patientModel.GetPatient(appointment.PatientId);
            User user = _userModelView.GetUser(patient.UserId);


            Debug.WriteLine(appointment.ToString());
            Debug.WriteLine(doctor.ToString());
            Debug.WriteLine(user.ToString());


            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Config.ROMANIA_TIMEZONE);

            string appointmentCalcelNotificationMessage = GetAppointmentCancelNotificationMessage(user.Name, appointment.AppointmentDateTime.ToString(), appointment.Location);
            int notificationId = _notificationModel.AddNotification(new Notification(doctor.UserId, currentDateTime, appointmentCalcelNotificationMessage));


        }



        public void DeleteUpcomingAppointmentNotification(int appointmentId)
        {
            Appointment appointment = _appointmentModel.GetAppointment(appointmentId);

            AppointmentNotification appointmentNotification = _notificationModel.GetNotificationAppointmentByAppointmentId(appointmentId);
            _notificationModel.deleteNotification(appointmentNotification.NotificationId);
        }

        //private void deleteUpcomingAppointmentNotification(int appo)

        public void DeleteAppointment(int userId)
        {
            AddCancelAppointmentNotification(HARDCODED_APPOINTMENT_ID);
            DeleteUpcomingAppointmentNotification(HARDCODED_APPOINTMENT_ID);

        }

        public void AddTreatment(int userId)
        {
            //_notificationModel.deleteNotification(notificationId);
        }

        public void AddReview(int userId)
        {
            //_notificationModel.deleteNotification(notificationId);
        }
    }
}