using AbidiHiringTask.Application.Repositories;
using HiringTask.Domain.Data.Context;
using HiringTask.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.UnitOfWorkPattern
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HiringTaskDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository {get; private set;}

        public IEducationalDocRepository EducationalDocRepository { get; private set; }

        public UnitOfWork(HiringTaskDbContext dbContext)
        {
            _dbContext = dbContext;
            EmployeeRepository = new EmployeeRepository(_dbContext);
            EducationalDocRepository = new EducationalDocRepository(_dbContext);
        }

        public async Task CompleteTaskAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
