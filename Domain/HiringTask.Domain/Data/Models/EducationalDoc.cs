using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringTask.Domain.Data.Models
{
    public class EducationalDoc : BasicModel
    {
        public Guid EmployeeId { get; set; }
        public string FileAddress { get; set; }


        public Employee Employee { get; set; }
    }
}
