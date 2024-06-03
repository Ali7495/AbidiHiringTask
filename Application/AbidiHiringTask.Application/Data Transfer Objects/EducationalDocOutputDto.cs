using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.Data_Transfer_Objects
{
    public class EducationalDocOutputDto : BasicOutputDto
    {
        public Guid EmployeeId { get; set; }
        public string FileAddress { get; set; }


        public EmployeeOutputDto Employee { get; set; }
    }
}
