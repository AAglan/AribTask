using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Models
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public string ImagePath { get; set; }

        // Foreign key to represent the department the employee belongs to
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        // Foreign key to represent the manager of the employee
        [ForeignKey("Manager")]
        public int? ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        public ICollection<EmployeeTask> EmployeeTasks { get; set; }

    }
}
