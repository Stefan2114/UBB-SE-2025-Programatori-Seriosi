using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;


public class Treatment { }
public class Review { } // mock for code purposes


public class MedicalRecord
{
    public int Id { get; set; }
    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    public List<Treatment> Treatments { get; set; } = new List<Treatment>();
    public List<Review> Reviews { get; set; } = new List<Review>();

    public MedicalRecord(int id, int patientId, int doctorId)
    {
        Id = id;
        PatientId = patientId;
        DoctorId = doctorId;
    }

    public override string ToString()
    {
        return $"[MedicalRecord] ID: {Id}, Patient ID: {PatientId}, Doctor ID : {DoctorId}";
    }
}
