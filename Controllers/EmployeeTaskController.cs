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
using Microsoft.AspNetCore.Identity;

namespace AribTask.Controllers
{
	public class EmployeeTaskController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IBaseRepo<EmployeeTask> _EmployeeTaskRepo;
		private readonly IBaseRepo<Employee> _EmployeeRepo;
		private readonly IMapper _Mapper;
		private readonly UserManager<IdentityUser> userManager;
		public EmployeeTaskController(ApplicationDbContext context, IBaseRepo<EmployeeTask> employeeTaskRepo, IMapper mapper, IBaseRepo<Employee> _EmployeeRepo, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_EmployeeTaskRepo = employeeTaskRepo;
			_Mapper = mapper;
			_EmployeeRepo = _EmployeeRepo;
			userManager = userManager;
			
		}

		// GET: EmployeeTask
		public IActionResult Index()
		{
			try
			{
				var EmpTasks = _EmployeeTaskRepo.GetAll();
				var Model = _Mapper.Map<List<EmployeeTaskDto>>(EmpTasks);
				var Employees = _context.Employees.Include(e => e.Manager).ToList();
				var currentUser = User.Identity.Name; // Get the username of the current user
				var EmployeeTask = Employees.FirstOrDefault(x => x.UserName != null && x.UserName.Trim() == currentUser.Trim());
				if (!EmployeeTask.IsManger)
				{
					Model = Model.Where(x => x.EmployeeId == EmployeeTask.Id).ToList();
				}
				else {
					Model = Model.Where(x => x.ManagerId == EmployeeTask.Id).ToList();
				}
				return View(Model);
			}
			catch (Exception)
			{
				return View();
			}

		}

		// GET: EmployeeTask/Details/5
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			//var employeeTaskDto = await _context.EmployeeTaskDto
			//		.FirstOrDefaultAsync(m => m.Id == id);
			//if (employeeTaskDto == null)
			//{
			//	return NotFound();
			//}
			//return View(employeeTaskDto);

			EmployeeTask employeeTask = _EmployeeTaskRepo.GetById(id.Value);
			EmployeeTaskDto Model = _Mapper.Map<EmployeeTaskDto>(employeeTask);
			return View(Model);
		}

		// GET: EmployeeTask/Create
		public IActionResult Create()
		{
		

			
			var Status = Enum.GetValues(typeof(Status))
						.Cast<Status>()
						.Select(g => new SelectListItem
						{
							Text = g.ToString(),
							Value = ((int)g).ToString()
						})
						.ToList();
	
			ViewBag.StatesList = Status;
			var employees = _context.Employees.ToList();
			var currentUser = User.Identity.Name; // Get the username of the current user
			var EmployeeTask  = employees.FirstOrDefault(x =>x.UserName!=null&& x.UserName.Trim() == currentUser.Trim());
			ViewBag.ShowManger = false;

			if (EmployeeTask.IsManger)
			{
				ViewBag.ShowManger = true;
				ViewBag.ManagerId = EmployeeTask.Id;
				employees = employees.Where(x => x.DepartmentId == EmployeeTask.DepartmentId && x.Id != EmployeeTask.Id).ToList();
			}
			ViewBag.employessDropdownlist = new SelectList(employees, "Id", "FirstName") ;
				return View();
		}

		// POST: EmployeeTask/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create( EmployeeTaskDto employeeTaskDto)
		{
			try
			{
				if (ModelState.IsValid)
				{
					
						EmployeeTask employeeTask = _Mapper.Map<EmployeeTask>(employeeTaskDto);
						employeeTask.SetBasicData(CRUD_OperationType.Create);
					employeeTask.Manager = _context.Employees.Find(employeeTask.ManagerId);
					employeeTask.Employee = _context.Employees.Find(employeeTask.EmployeeId);

					_EmployeeTaskRepo.Add(employeeTask);

				
				}

			}
			catch (Exception)
			{
			}

			return RedirectToAction("Index");
		}

		// GET: EmployeeTask/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var Status = Enum.GetValues(typeof(Status))
				.Cast<Status>()
				.Select(g => new SelectListItem
				{
					Text = g.ToString(),
					Value = ((int)g).ToString()
				})
				.ToList();
			ViewBag.StatesList = Status;
			ViewBag.ShowManger = false;
			var employees = _context.Employees.ToList();

			var currentUser = User.Identity.Name; // Get the username of the current user
			var EmployeeTask = employees.FirstOrDefault(x => x.UserName != null && x.UserName.Trim() == currentUser.Trim());
			if (EmployeeTask.IsManger)
			{
				ViewBag.ShowManger = true;
				employees = employees.Where(x => x.DepartmentId == EmployeeTask.DepartmentId && x.Id != EmployeeTask.Id).ToList();
			}
			ViewBag.employessDropdownlist = new SelectList(employees, "Id", "FirstName");
			EmployeeTask employeeTask = _EmployeeTaskRepo.GetById(id.Value);
			if (employeeTask == null)
			{
				return NotFound();
			}
			EmployeeTaskDto Model = _Mapper.Map<EmployeeTaskDto>(employeeTask);
			return View(Model);

		}

		// POST: EmployeeTask/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Subject,Body,ManagerId,EmployeeId,Status,Id,CreationDate,UpdatedDate,CreationUserId,UpdatedUserId")] EmployeeTaskDto employeeTaskDto)
		{
			if (id != employeeTaskDto.Id)
			{
				return NotFound();
			}

			//if (ModelState.IsValid)
			//{
			//	try
			//	{
			//		_context.Update(employeeTaskDto);
			//		await _context.SaveChangesAsync();
			//	}
			//	catch (DbUpdateConcurrencyException)
			//	{
			//		if (!EmployeeTaskDtoExists(employeeTaskDto.Id))
			//		{
			//			return NotFound();
			//		}
			//		else
			//		{
			//			throw;
			//		}
			//	}
			//	return RedirectToAction(nameof(Index));
			//}
			//return View(employeeTaskDto);
			try
			{
				if (ModelState.IsValid)
				{
					EmployeeTask employeeTask = _Mapper.Map<EmployeeTask>(employeeTaskDto);
					employeeTask.SetBasicData(CRUD_OperationType.Update);
					employeeTask.Employee = _context.Employees.Find(employeeTask.ManagerId);
					employeeTask.Employee = _context.Employees.Find(employeeTask.EmployeeId);
					_EmployeeTaskRepo.Update(employeeTask);

				}
				else
				{

				}
			}
			catch (Exception ex)
			{
			}


			return RedirectToAction("Index");
		}

		// GET: EmployeeTask/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			EmployeeTask employeeTask = _EmployeeTaskRepo.GetById(id.Value);
			if (employeeTask == null)
			{
				return NotFound();
			}

			EmployeeTaskDto model = _Mapper.Map<EmployeeTaskDto>(employeeTask);

			return View(model);
		}

		// POST: EmployeeTask/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var employeeTaskD = _EmployeeTaskRepo.GetById(id);
			_EmployeeTaskRepo.Delete(employeeTaskD);

			return RedirectToAction(nameof(Index));
		}

		//private bool EmployeeTaskDtoExists(int id)
		//{
		//	return _context.EmployeeTaskDto.Any(e => e.Id == id);
		//}
	}
}
