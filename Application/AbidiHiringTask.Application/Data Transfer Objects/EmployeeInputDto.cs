using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.Data_Transfer_Objects
{
    public class EmployeeInputDto
    {
        public EmployeeInputDto()
        {
            FileAddresses = new();
        }

        [Required(ErrorMessage = "نام اجباری است")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "نام خانوادگی اجباری است")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "کد پرسنلی اجباری است")]
        public string PersonalCode { get; set; }

        [Required(ErrorMessage = "کد ملی اجباری است")]
        public string NationalCode { get; set; }

        public List<string> FileAddresses { get; set; }

    }
}
