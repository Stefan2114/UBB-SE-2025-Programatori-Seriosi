using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppointmentNotification : Notification
{
    public int AppointmentId { get; set; }

    public AppointmentNotification(int id, DateTime deliveryDateTime, string message, int appointmentId)
        : base(id, deliveryDateTime, message)
    {
        AppointmentId = appointmentId;
    }

    public override string ToString()
    {
        return $"[AppointmentNotification] ID: {Id}, Appointment ID: {AppointmentId}, Delivery: {DeliveryDateTime}, Message: {Message}";
    }
}
