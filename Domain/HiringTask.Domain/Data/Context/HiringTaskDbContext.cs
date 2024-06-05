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

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.NationalCode)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e=> e.PersonalCode)
                .IsUnique();


            modelBuilder.Entity<Employee>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<EducationalDoc>().HasQueryFilter(e => !e.IsDeleted);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added && entry.Entity is BasicModel added)
                {
                    added.CreatedDate = DateTime.Now;
                }
                else if(entry.State == EntityState.Modified && entry.Entity is BasicModel updated)
                {
                    updated.UpdatedDate = DateTime.Now;
                }
            }
        }
    }
}
