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

            RuleFor(e => e.NationalCode)
                .Must(IsValidNationalCode)
                .WithMessage("کد ملی معتبر نیست");
        }



        public static bool IsValidNationalCode(string nationalCode)
        {
            if (nationalCode.Length != 10)
            {
                return false;
            }


            foreach (char digit in nationalCode)
            {
                if (!char.IsDigit(digit))
                {
                    return false;
                }
            }

            int sum = 0;
            for (int i = 0; i < nationalCode.Length - 1; i++)
            {
                sum += int.Parse(nationalCode[i].ToString()) * (10 - i);
            }

            int controlDigit = int.Parse(nationalCode[9].ToString());
            int calculatedControlDigit = sum % 11;

            return (calculatedControlDigit < 2 && controlDigit == calculatedControlDigit) || (calculatedControlDigit >= 2 && controlDigit + calculatedControlDigit == 11);
        }
    }
}
