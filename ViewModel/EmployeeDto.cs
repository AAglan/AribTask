using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace AribTask.ViewModel
{
	public class EmployeeDto:BasicModel
	{
 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
    [NotMapped]
    public string FullName { get { return FirstName + " " + LastName; } }
    public string ImageFileName { get; set; } // Add a new property for the unique file name
    
    public IFormFile ImageFile { get; set; }
    public int? ManagerId { get; set; }
    public int DepartmentId { get; set; }

    public EmployeeDto Manager { get; set; }
    public ICollection<EmployeeTaskDto> EmployeeTasks { get; set; }
  }
}
