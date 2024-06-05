using AbidiHiringTask.Application.Common_Tools;
using AbidiHiringTask.Application.Data_Transfer_Objects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.Services_Interfaces
{
    public interface IEmployeeServices
    {
        Task<ResultModel> AddEmployeeAsync(EmployeeInputDto employeeInput, List<IFormFile> files, CancellationToken cancellationToken);
        Task<List<EmployeeOutputDto>> GetEmployeesAsync(CancellationToken cancellationToken);
        Task<ResultModel> UpdateEmployeeAsync(Guid id, EmployeeInputDto employeeInput, CancellationToken cancellationToken);
        Task<ResultModel> DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken);
    }
}
