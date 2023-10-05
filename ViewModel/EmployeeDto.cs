using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AribTask.ViewModel
{
	public class EmployeeDto:BasicModel
	{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [NotMapped]
    public string FullName { get { return FirstName + " " + LastName; } }
    public decimal Salary { get; set; }
    public string ImagePath { get; set; }
    public int ManagerId { get; set; }
    public int DepartmentId { get; set; }
  }
}
