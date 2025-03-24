using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Notification
{
    public int Id { get; set; }
    public DateTime DeliveryDateTime { get; set; }
    public string Message { get; set; }

    public Notification(int id, DateTime deliveryDateTime, string message)
    {
        Id = id;
        DeliveryDateTime = deliveryDateTime;
        Message = message;
    }

    public override string ToString()
    {
        return $"[Notification] ID: {Id}, Delivery: {DeliveryDateTime}, Message: {Message}";
    }
}
