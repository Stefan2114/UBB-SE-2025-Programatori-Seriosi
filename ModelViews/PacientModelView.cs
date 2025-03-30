using Team3.Entities;
using Team3.Models;

namespace Team3.ViewModels
{
    public class PatientModelView
    {
        private readonly PatientModel _patientModel;

        public PatientModelView()
        {
            _patientModel = PatientModel.Instance;
        }

        public Patient GetPatient(int pID)
        {
            return _patientModel.GetPatient(pID);
        }
    }
}
