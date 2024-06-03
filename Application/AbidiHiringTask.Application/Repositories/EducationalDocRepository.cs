using HiringTask.Domain.Data.Context;
using HiringTask.Domain.Data.Models;
using HiringTask.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbidiHiringTask.Application.Repositories
{
    public class EducationalDocRepository : Repository<EducationalDoc>, IEducationalDocRepository
    {
        public EducationalDocRepository(HiringTaskDbContext dbContext) : base(dbContext)
        {
        }
    }
}
