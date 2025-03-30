using System;
using Team3.Entities;

namespace Team3.Models
{
    public class DoctorModelView
    {
        private readonly DoctorModel _doctorModel;

        public DoctorModelView()
        {
            _doctorModel = DoctorModel.Instance;
        }

        public Doctor GetDoctor(int dID)
        {
            return _doctorModel.GetDoctor(dID);
        }
    }
}
