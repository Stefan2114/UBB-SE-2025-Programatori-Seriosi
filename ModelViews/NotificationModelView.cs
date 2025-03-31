using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Team3.Entities;
using Team3.Models;

namespace Team3.ModelViews
{


    public class NotificationModelView
    {

        private readonly NotificationModel _notificationModel;
        private readonly AppointmentModel _appointmentModel;
        private readonly DoctorModel _doctorModel;
        private readonly UserModel _userModel;

        private readonly static string UPCOMING_APPOINTMENT_NOTIFICATION_TEMPLATE = "You have an appointment tommrow at @date with @doctor at location @location";
        private readonly static string APPOINTMENT_CANCEL_NOTIFICATION_TEMPLATE = "The appointent that was scheduled for @date with @doctor at location @location was canceled";
        private readonly static string REVIEW_NOTIFICATION_TEMPLATE = "A review for doctor @doctor was added: @message; number of starts: @nrStarts";
        private readonly static string MEDICATION_REMINDER_NOTIFICATION_TEMPLATE = "Reminder to take @drug with quantity: @quantity. @administration";





        public List<Notification> Notifications { get; private set; }

        public NotificationModelView()
        {
            _notificationModel = NotificationModel.Instance;
            _appointmentModel = AppointmentModel.Instance;
            Notifications = new List<Notification>();
        }

        public void LoadNotifications(int userId)
        {

            DateTime currentDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Config.ROMANIA_TIMEZONE);

            List<Notification> notifications = _notificationModel.GetUserNotifications(userId);
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

            Appointment appointment = new Appointment(4, 1, userId, currentDateTime.AddDays(1), "FSEGA");

            _appointmentModel.AddAppointment(appointment);
            AddUpcomingAppointmentNotification(4);

        }


        public void AddUpcomingAppointmentNotification(int appointmentId)
        {
            Appointment appointment = _appointmentModel.GetAppointment(appointmentId);
            Doctor doctor = _doctorModel.GetDoctor(appointment.DoctorId);
            User user = _userModel.GetUser(doctor.UserId);

            string upcomingAppointmentNotificationMessage = UPCOMING_APPOINTMENT_NOTIFICATION_TEMPLATE.Replace("@date", appointment.AppointmentDateTime.ToString());
            int notificationId = _notificationModel.AddNotification(new Notification(user.Id,appointment.AppointmentDateTime.AddDays(-1), upcomingAppointmentNotificationMessage));
            _notificationModel.AddAppointmentNotification(notificationId, appointmentId);
        }

        public void DeleteAppointment(int userId)
        {
            _notificationModel.deleteNotification(userId);
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