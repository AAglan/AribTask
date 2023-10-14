using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.ViewModels
{
	public class EmployeeTaskDto:BasicModel
	{
    public string Subject { get; set; }
    public string Body { get; set; }
    public int ManagerId { get; set; }
    public int EmployeeId { get; set; }
    public int Status { get; set; }
    [NotMapped]
    public string EmployeeName { get; set; }
    [NotMapped]
    public string MangerName { get; set; }


  }
}
