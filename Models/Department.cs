using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Models
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }

        public decimal DepartmentCost { get; set; }

        public decimal EmployeesCount { get; set; }

        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public virtual ICollection<Employee> Managers { get; set; }


    }



}
