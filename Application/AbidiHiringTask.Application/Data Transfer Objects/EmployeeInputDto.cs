using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.Data_Transfer_Objects
{
    public class EmployeeInputDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public string NationalCode { get; set; }

        public List<string> FileAddresses { get; set; }

    }
}
