using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppointmentNotification
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public int NotificationId { get; set; }


   public AppointmentNotification(int id, int appointmentId, int notificationId)
    {
        Id = id;
        AppointmentId = appointmentId;
        NotificationId = notificationId;
    }

    public override string ToString()
    {
        return $"[AppointmentNotification] ID: {Id}, Appointment ID: {AppointmentId}, Notification ID: {NotificationId}";
    }
}
