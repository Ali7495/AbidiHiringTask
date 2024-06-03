using AbidiHiringTask.Application.Services;
using AbidiHiringTask.Application.Services_Interfaces;
using HiringTask.Domain.Interfaces;
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
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private IEmployeeServices _employeeServices;

        [SetUp]
        public void SetUp()
        {
            _mockEmployeeRepository= new Mock<IEmployeeRepository>();
            _employeeServices = CreateEmployeeService();
        }

        private IEmployeeServices CreateEmployeeService()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddTransient<IEmployeeRepository>(provider=> _mockEmployeeRepository.Object);

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<IEmployeeServices>();
        }


    }
}
