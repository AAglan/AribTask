using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.ViewModels
{
	
		public class EmployeesDepartmentDto
		{
			public int ManagerId { get; set; }
			public int DepartmentId { get; set; }
			public EmployeeDto Manager { get; set; }
			public DepartmentDto Department { get; set; }
		}

}
