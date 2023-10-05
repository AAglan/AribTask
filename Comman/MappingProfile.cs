using AribTask.Models;
using AribTask.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AribTask.Comman
{
	public class MappingProfile : Profile
  {
    public MappingProfile()
    {

			CreateMap<Employee, EmployeeDto>().ReverseMap();
			CreateMap<Department, DepartmentDto>().ReverseMap();
			CreateMap<EmployeeTask, EmployeeTaskDto>().ReverseMap();







		}
  }
}
