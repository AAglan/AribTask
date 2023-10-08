using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AribTask.Data;
using AribTask.ViewModel;
using AribTask.Service;
using AribTask.Models;
using AutoMapper;
using AribTask.Comman;

namespace AribTask.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IBaseRepo<Employee> _EmployeeRepo;
		private readonly IMapper _Mapper;
		private readonly IBaseRepo<Department> _DepartmentRepo;
		public EmployeeController(ApplicationDbContext context, IBaseRepo<Employee> employeeRepo, IMapper mapper, IBaseRepo<Department> DepartmentRepo)
		{
			_context = context;
			_EmployeeRepo = employeeRepo;
			_Mapper = mapper;
			_DepartmentRepo = DepartmentRepo;
		}


		// GET: Employee
		public IActionResult Index()
		{
			try
			{
				var EmpTasks = _EmployeeRepo.GetAll();
				var Model = _Mapper.Map<List<EmployeeDto>>(EmpTasks);
				return View(Model);
			}
			catch (Exception)
			{
				return View();
			}

		}

		// GET: Employee/Details/5
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}



			Employee employee = _EmployeeRepo.GetById(id.Value);
			EmployeeDto Model = _Mapper.Map<EmployeeDto>(employee);
			return View(Model);
		}

		// GET: Employee/Create
		public IActionResult Create()
		{
			var employees = _EmployeeRepo.GetAll();
			var departments = _DepartmentRepo.GetAll();
			ViewBag.ManagerId = new SelectList(employees, "Id", "FirstName",null);
			ViewBag.DepartmentId = new SelectList(departments, "Id", "Name");
			return View();
		}
		// POST: Employee/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("FirstName,LastName,Salary,ImagePath,ManagerId,DepartmentId,Id,CreationDate,UpdatedDate,CreationUserId,UpdatedUserId")] EmployeeDto employeeDto)
		{
			try
			{
				if (ModelState.IsValid)
				{
						Employee Employee = _Mapper.Map<Employee>(employeeDto);
						Employee.SetBasicData(CRUD_OperationType.Create);
						_EmployeeRepo.Add(Employee);
				}

			}
			catch (Exception ex)
			{
				var xx = ex.ToString();
			}

			return View(employeeDto);
		}

		// GET: Employee/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Employee employee = _EmployeeRepo.GetById(id.Value);
			if (employee == null)
			{
				return NotFound();
			}
			EmployeeDto Model = _Mapper.Map<EmployeeDto>(employee);
			return View(Model);

		}


		// POST: Employee/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("FirstName,LastName,Salary,ImagePath,ManagerId,DepartmentId,Id,CreationDate,UpdatedDate,CreationUserId,UpdatedUserId")] EmployeeDto employeeDto)
		{
			if (id != employeeDto.Id)
			{
				return NotFound();
			}


			try
			{
				if (ModelState.IsValid)
				{
					Employee employee = _Mapper.Map<Employee>(employeeDto);
					employee.SetBasicData(CRUD_OperationType.Update);

					_EmployeeRepo.Update(employee);

				}
				else
				{

				}
			}
			catch (Exception)
			{
			}


			return View(employeeDto);
		}

		// GET: Employee/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Employee employee = _EmployeeRepo.GetById(id.Value);
			if (employee == null)
			{
				return NotFound();
			}

			EmployeeDto model = _Mapper.Map<EmployeeDto>(employee);

			return View(model);
		}

		// POST: Employee/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var employee = _EmployeeRepo.GetById(id);
			_EmployeeRepo.Delete(employee);

			return RedirectToAction(nameof(Index));
		}

		//private bool EmployeeDtoExists(int id)
		//{
		//	return _context.EmployeeDto.Any(e => e.Id == id);
		//}
	}
}
