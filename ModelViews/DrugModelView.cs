using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3.Entities;
using Team3.Models;

namespace Team3.ModelViews
{
    public class DrugModelView
    {
        private readonly DrugModel _drugModel;

        public DrugModelView()
        {
            _drugModel = DrugModel.Instance;
        }

        public Drug getDrug(int mrId)
        {
            return _drugModel.getDrug(mrId);
        }
    }
}
