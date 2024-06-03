using HiringTask.Domain.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringTask.Domain.Data.Context
{
    public class HiringTaskDbContext : DbContext
    {
        public HiringTaskDbContext(DbContextOptions<HiringTaskDbContext> options) : base(options) 
        { 

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EducationalDoc> EducationalDocs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EducationalDoc>()
                .HasOne(e => e.Employee)
                .WithMany(e => e.EducationalDocs)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee>()
                .Property(e => e.PersonalCode).HasMaxLength(10);


            modelBuilder.Entity<Employee>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<EducationalDoc>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
