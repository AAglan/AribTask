using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Models
{
    public class EmployeeTask : BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Status { get; set; }

        // Foreign key to represent the employee responsible for the task
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }


        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        public Employee Manager { get; set; }
    }
}
