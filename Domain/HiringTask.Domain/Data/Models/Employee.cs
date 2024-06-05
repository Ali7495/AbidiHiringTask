using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringTask.Domain.Data.Models
{
    public class Employee : BasicModel
    {
        public Employee()
        {
            EducationalDocs = new();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public string NationalCode { get; set; }



        public List<EducationalDoc> EducationalDocs { get; set; }
    }
}
