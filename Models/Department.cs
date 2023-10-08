using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Models
{
    //public class Department : BaseEntity
    //{
    //    public string Name { get; set; }

    //    public decimal DepartmentCost { get; set; }

    //    public decimal EmployeesCount { get; set; }

    //    //[ForeignKey("Manager")]
    //    //public int ManagerId { get; set; }
    //    //public virtual ICollection<Employee> Managers { get; set; }


    //}
  public class Department : BaseEntity
  {
    public string Name { get; set; }
    public decimal DepartmentCost { get; set; }
    public decimal EmployeesCount { get; set; }

    // Navigation property to represent the employees in the department
    public virtual ICollection<Employee> Employees { get; set; }
  }



}
