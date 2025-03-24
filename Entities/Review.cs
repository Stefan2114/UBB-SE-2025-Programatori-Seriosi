using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Team3.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int medicalRecordId {  get; set; }
        public string Message { get; set; }
        public int NrStars { get; set; }


        public Review(int id,int medicalRecordId, string message, int nrStars)
        {
            this.Id = id;
            this.medicalRecordId = medicalRecordId;
            this.Message = message;
            this.NrStars = nrStars;
        }

        public Review()
        {
        }

        override
        public string ToString()
        {
            return $"Id: {Id}, MedicalRecordId: {medicalRecordId}, Message: {Message}, Stars: {NrStars}";
        }
    }
}
