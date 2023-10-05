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
       [ForeignKey("Manager")]
        public int ManagerId { get; set; }
       [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual Department Department { get; set; }
        public Employee()
        {

        }
    }
}
