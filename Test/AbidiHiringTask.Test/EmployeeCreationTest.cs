using AbidiHiringTask.Application.Common_Tools;
using AbidiHiringTask.Application.Data_Transfer_Objects;
using AbidiHiringTask.Application.Services;
using AbidiHiringTask.Application.Services_Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HiringTask.Domain.Data.Models;
using HiringTask.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Test
{
    public class EmployeeCreationTest
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private IEmployeeServices _employeeServices;
        private Mock<IMapper> _mapper;
        private Mock<IValidator<EmployeeInputDto>> _validator;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _validator = new Mock<IValidator<EmployeeInputDto>>();
            _employeeServices = CreateEmployeeService();
        }

        private IEmployeeServices CreateEmployeeService()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddTransient<IUnitOfWork>(provider => _unitOfWork.Object);
            services.AddTransient<IMapper>(provider => _mapper.Object);
            services.AddTransient<IValidator<EmployeeInputDto>>(provider => _validator.Object);

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<IEmployeeServices>();
        }

        [Test]
        public void CreateEmployee_ValidData_ShouldCreateAndSaveEmployee()
        {
            // Arrange

            EmployeeInputDto employeeInput = new()
            {
                FirstName = "Ali",
                LastName = "Fakhri",
                NationalCode = "1234567890",
                PersonalCode = "12345"
            };

            CancellationToken cancellationToken = CancellationToken.None;

            _mapper.Setup(m => m.Map<Employee>(employeeInput)).Returns(new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = employeeInput.FirstName,
                LastName = employeeInput.LastName,
                PersonalCode = employeeInput.PersonalCode,
                NationalCode = employeeInput.NationalCode,
                CreatedDate = DateTime.Now,
                EducationalDocs = new List<EducationalDoc>()
            });

            List<IFormFile> files = new()
            {
                new FormFile(new MemoryStream(new byte[] { }), 0, 0, "file1", "file1.txt"),
                new FormFile(new MemoryStream(new byte[] { }), 0, 0, "file2", "file2.pdf"),
            };

            _unitOfWork.Setup(u => u.EmployeeRepository.AddAsync(It.IsAny<Employee>(), cancellationToken))
                .Returns(Task.CompletedTask);

            _unitOfWork.Setup(u => u.CompleteTaskAsync(cancellationToken)).Returns(Task.CompletedTask);

            _validator.Setup(v => v.Validate(It.IsAny<EmployeeInputDto>())).Returns(new ValidationResult());

            // Act

            var result = _employeeServices.AddEmployeeAsync(employeeInput, files, cancellationToken).Result;

            // Assert

            Assert.NotNull(result);
            Assert.AreEqual(ResultStatus.Success, result.Status);

            _mapper.Verify(m => m.Map<Employee>(employeeInput), Times.Once());

            _unitOfWork.Verify(u => u.EmployeeRepository.AddAsync(It.Is<Employee>(e =>
            e.Id != Guid.Empty), cancellationToken), Times.Once
            );
        }
    }
}
