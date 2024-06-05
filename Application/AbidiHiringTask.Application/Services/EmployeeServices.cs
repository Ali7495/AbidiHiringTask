using AbidiHiringTask.Application.Common_Tools;
using AbidiHiringTask.Application.Data_Transfer_Objects;
using AbidiHiringTask.Application.Services_Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HiringTask.Domain.Data.Models;
using HiringTask.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<EmployeeInputDto> _validator;

        public EmployeeServices(IMapper mapper, IUnitOfWork unitOfWork, IValidator<EmployeeInputDto> validator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ResultModel> AddEmployeeAsync(EmployeeInputDto employeeInput, List<IFormFile> files, CancellationToken cancellationToken)
        {
            try
            {
                ResultModel resultModel = ValidateEmployee(employeeInput);
                if (resultModel.Status == ResultStatus.Failed)
                {
                    return resultModel;
                }

                resultModel = await ValidateAndSaveFileAsync(files, employeeInput);
                if (resultModel.Status != ResultStatus.Ok)
                {
                    return resultModel;
                }

                Employee employee = _mapper.Map<Employee>(employeeInput);

                await _unitOfWork.EmployeeRepository.AddAsync(employee, cancellationToken);

                await _unitOfWork.CompleteTaskAsync(cancellationToken);

                resultModel.Status = ResultStatus.Success;
                resultModel.Message = "با موفقیت ذخیره شد";

                return resultModel;
            }
            catch (DbUpdateException ex) when (IsUniqueConstraintViolation(ex))
            {
                return new() { Status = ResultStatus.Failed, Message = "کد ملی یا کد پرسنلی تکراری وارد شده است" };
            }
        }

        public async Task<ResultModel> DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken)
        {
            Employee employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id, cancellationToken);

            employee.IsDeleted = false;

            await _unitOfWork.EmployeeRepository.UpdateAsync(employee);
            await _unitOfWork.CompleteTaskAsync(cancellationToken);

            return new() { Status = ResultStatus.Success, Message = "با موفقیت حذف شد" };
        }

        public async Task<List<EmployeeOutputDto>> GetEmployeesAsync(CancellationToken cancellationToken)
        {
            IEnumerable<Employee> employees = await _unitOfWork.EmployeeRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<List<EmployeeOutputDto>>(employees);
        }

        public async Task<ResultModel> UpdateEmployeeAsync(Guid id, EmployeeInputDto employeeInput, CancellationToken cancellationToken)
        {
            ResultModel resultModel = ValidateEmployee(employeeInput);
            if (resultModel.Status == ResultStatus.Failed)
            {
                return resultModel;
            }

            Employee employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id,cancellationToken);

            employee.FirstName= employeeInput.FirstName;
            employee.LastName= employeeInput.LastName;
            employee.PersonalCode= employeeInput.PersonalCode;
            employee.NationalCode= employeeInput.NationalCode;

            await _unitOfWork.EmployeeRepository.UpdateAsync(employee);
            await _unitOfWork.CompleteTaskAsync(cancellationToken);

            resultModel.Status = ResultStatus.Success;
            resultModel.Message = "با موفقیت ویرایش شد";

            return resultModel;
        }


        #region inner methods

        private ResultModel ValidateEmployee(EmployeeInputDto employeeInput)
        {
            ValidationResult validationResult = _validator.Validate(employeeInput);

            if (!validationResult.IsValid)
            {
                return new() { Status = ResultStatus.Failed, Message = string.Join(',',validationResult.Errors.Select(e=> e.ErrorMessage).ToList()) };
            }

            return new() { Status = ResultStatus.Ok };
        }

        private async Task<ResultModel> ValidateAndSaveFileAsync(List<IFormFile> files, EmployeeInputDto employeeInputDto)
        {
            string[] allowedExtentions = new[] { ".txt", ".pdf" };
            int maxFileSize = 500 * 1024;
            List<string> savedFileAddresses = new();

            StringBuilder errorMessages = null;

            foreach (var file in files)
            {
                string extension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtentions.Contains(extension))
                {
                    if (errorMessages == null)
                    {
                        errorMessages = new StringBuilder();
                    }

                    errorMessages.AppendLine("فقط فایل‌های متنی و PDF مجاز هستند");
                }
                else if (file.Length > maxFileSize)
                {
                    if (errorMessages == null)
                    {
                        errorMessages = new StringBuilder();
                    }

                    errorMessages.AppendLine("فایل‌های بیشتر از 500 کیلوبایت نمی‌توان ذخیره کرد.");
                }
                else
                {
                    string savedPath = await SaveFileAsync(file);
                    savedFileAddresses.Add(savedPath);
                }
            }

            if (errorMessages != null)
            {
                return new ResultModel { Status = ResultStatus.Failed, Message = errorMessages.ToString() };
            }

            // Check if FileAddresses is initialized and has the same count as savedFileAddresses
            if (employeeInputDto.FileAddresses == null || employeeInputDto.FileAddresses.Count != savedFileAddresses.Count)
            {
                return new ResultModel { Status = ResultStatus.Failed, Message = "Unexpected error occurred." };
            }

            for (int i = 0; i < employeeInputDto.FileAddresses.Count; i++)
            {
                employeeInputDto.FileAddresses[i] = savedFileAddresses[i];
            }

            return new() { Status = ResultStatus.Ok };
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            string uploadedAddress = Path.Combine(Directory.GetCurrentDirectory(),"UploadedFiles");

            if (!Directory.Exists(uploadedAddress))
            {
                Directory.CreateDirectory(uploadedAddress);
            }

            string savedAddress = Path.Combine(uploadedAddress,file.Name);

            using (FileStream stream = new(savedAddress,FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return savedAddress;
        }


        private bool IsUniqueConstraintViolation(DbUpdateException ex)
        {
            return ex.InnerException is SqlException sqlEx &&
           (sqlEx.Number == 2601 || sqlEx.Number == 2627);
        }

        #endregion
    }
}
