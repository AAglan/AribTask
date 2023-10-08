using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Models
{
  [Keyless]
	public class EmployeesDepartment
  {
    [ForeignKey("Manager")]
    public int ManagerId { get; set; }

    [ForeignKey("Department")]
    public int DepartmentId { get; set; }

    // Navigation properties to represent the managers and departments
    public virtual Employee Manager { get; set; }
    public virtual Department Department { get; set; }
  }
}
