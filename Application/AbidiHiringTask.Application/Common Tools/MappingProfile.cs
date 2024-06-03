using AbidiHiringTask.Application.Data_Transfer_Objects;
using AutoMapper;
using HiringTask.Domain.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.Common_Tools
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeOutputDto>();
            CreateMap<EmployeeInputDto, Employee>();


            CreateMap<EducationalDoc, EducationalDocOutputDto>();
        }
    }
}
