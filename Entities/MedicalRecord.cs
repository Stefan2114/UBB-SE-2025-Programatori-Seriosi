using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;


public class Treatment { }
public class Review { } // mock for code purposes


public class MedicalRecord
{
    public int Id { get; set; }
    public int PatientId { get; set; }

    public List<Treatment> Treatments { get; set; } = new List<Treatment>();
    public List<Review> Reviews { get; set; } = new List<Review>();

    public MedicalRecord(int id, int patientId)
    {
        Id = id;
        PatientId = patientId;
    }
 
}
