using AribTask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AribTask.ViewModel;

namespace AribTask.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<EmployeeTask> EmployeeTasks { get; set; }
		public DbSet<EmployeesDepartment> EmployeesDepartments { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
				: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<EmployeeTask>()
					.HasOne(e => e.Employee)
					.WithMany()
					.HasForeignKey(e => e.EmployeeId)
					.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<EmployeeTask>()
					.HasOne(e => e.Manager)
					.WithMany()
					.HasForeignKey(e => e.ManagerId)
					.OnDelete(DeleteBehavior.NoAction);
			modelBuilder.Entity<EmployeesDepartment>()
		.HasOne(e => e.Manager)
		.WithMany()
		.HasForeignKey(e => e.ManagerId)
		.OnDelete(DeleteBehavior.NoAction);

			modelBuilder.Entity<EmployeesDepartment>()
					.HasOne(e => e.Department)
					.WithMany()
					.HasForeignKey(e => e.DepartmentId)
					.OnDelete(DeleteBehavior.NoAction);

		}
		public DbSet<AribTask.ViewModel.EmployeeDto> EmployeeDto { get; set; }



	}
}
