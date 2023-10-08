using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.ViewModel
{
	public class DepartmentDto:BasicModel
	{
		public string Name { get; set; }
		public decimal DepartmentCost { get; set; }
		public decimal EmployeesCount { get; set; }
		public ICollection<EmployeeDto> Employees { get; set; }
	}
}
