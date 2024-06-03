using HiringTask.Domain.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringTask.Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}
