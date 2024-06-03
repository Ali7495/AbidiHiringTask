using AbidiHiringTask.Application.Common_Tools;
using AbidiHiringTask.Application.Common_Tools.Validations;
using AbidiHiringTask.Application.UnitOfWorkPattern;
using FluentValidation;
using HiringTask.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region Repositories

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            #region AutoMapper

            services.AddAutoMapper(typeof(MappingProfile));

            #endregion

            #region Validators

            services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();

            #endregion
        }
    }
}
