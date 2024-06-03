using AbidiHiringTask.Application.Data_Transfer_Objects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.Common_Tools.Validations
{
    public class EmployeeValidator : AbstractValidator<EmployeeInputDto>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.PersonalCode).MinimumLength(4).WithMessage("کد پرسنلی نمی تواند کمتر از 4 کاراکتر باشد")
                .MaximumLength(10).WithMessage("کد پرسنلی نمیتواند بیشتر از 10 کاراکتر باشد");
        }
    }
}
