using AribTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        //public DbSet<Employee> Employees { get; set; }
        //public DbSet<Department> Departments { get; set; }
        //public DbSet<EmployeeTask> EmployeeTasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EmployeeTask>()
            //    .HasOne(e => e.Employee)
            //    .WithMany()
            //    .HasForeignKey(e => e.EmployeeId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<EmployeeTask>()
            //    .HasOne(e => e.Manager)
            //    .WithMany()
            //    .HasForeignKey(e => e.ManagerId)
            //    .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
