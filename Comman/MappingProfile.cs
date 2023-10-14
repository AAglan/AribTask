using AribTask.Models;
using AribTask.ViewModels;
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

			CreateMap<Employee, EmployeeDto>().ForMember(dest => dest.ImageFile, opt => opt.Ignore()).ReverseMap();
			CreateMap<Department, DepartmentDto>().ReverseMap();
			CreateMap<EmployeeTask, EmployeeTaskDto>().ReverseMap();
      CreateMap<EmployeesDepartment, EmployeesDepartmentDto>()
            .ForMember(dest => dest.Manager, opt => opt.MapFrom(src => src.Manager))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ReverseMap();






    }
  }
}
