using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AribTask.ViewModel
{
	public class EmployeeDto:BasicModel
	{
    // Foreign key to AspNetUsers
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Salary { get; set; }
    [NotMapped]
    public string FullName { get { return FirstName + " " + LastName; } }
    public string ImageFileName { get; set; } // Add a new property for the unique file name
    
    public IFormFile ImageFile { get; set; }
    public int? ManagerId { get; set; }
    public int DepartmentId { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
   [DataType(DataType.Password)]
    public string Password { get; set; }

    public EmployeeDto Manager { get; set; }
    public ICollection<EmployeeTaskDto> EmployeeTasks { get; set; }
  }
}
