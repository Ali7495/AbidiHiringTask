using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.Data_Transfer_Objects
{
    public class EmployeeOutputDto : BasicOutputDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public string NationalCode { get; set; }


        public List<EducationalDocOutputDto> EducationalDocs { get; set; }
    }
}
