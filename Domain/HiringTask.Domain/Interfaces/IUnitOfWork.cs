using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringTask.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        IEducationalDocRepository EducationalDocRepository { get; }


        Task CompleteTaskAsync(CancellationToken cancellationToken);
        Task DisposeAsync();
    }
}
