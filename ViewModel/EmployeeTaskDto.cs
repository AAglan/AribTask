using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.ViewModel
{
	public class EmployeeTaskDto:BasicModel
	{
    public string Subject { get; set; }
    public string Body { get; set; }
    public int ManagerId { get; set; }
    public int EmployeeId { get; set; }
    public int Status { get; set; }
  }
}
