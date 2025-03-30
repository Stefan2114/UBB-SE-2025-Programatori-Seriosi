using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Models;
using Team3.Entities;
using System.Diagnostics;
using System.Collections;

namespace Team3.ModelViews
{
    public class TreatmentModelView
    {
        private readonly TreatmentModel _treatmentModel;

        public TreatmentModelView()
        {
            _treatmentModel = TreatmentModel.Instance;
        }

        public void addTreatment(Treatment treatment)
        {
            _treatmentModel.addTreatment(treatment);
        }
        public void addTreatmentButtonHandler(int id, int medicalrecordId)
        {
            Debug.WriteLine("Add button clicked");
            Treatment newTreatment = new Treatment(id, medicalrecordId);    
            
            addTreatment(newTreatment);
        }
    }
}
