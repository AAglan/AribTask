using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Models
{
    public class EmployeeTask:BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        [ForeignKey("Manager")]
        public int ManagerId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public int Status { get; set; }
        public Employee Manager { get; set; }
        public Employee Employee { get; set; }
    }
}
