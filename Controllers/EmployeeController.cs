using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AribTask.Data;
using AribTask.ViewModels;
using AribTask.Service;
using AribTask.Models;
using AutoMapper;
using AribTask.Comman;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AribTask.Controllers
{
	[Authorize(Roles = "Admin")]
	public class EmployeeController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IBaseRepo<Employee> _EmployeeRepo;
		private readonly IMapper _Mapper;
		private readonly IBaseRepo<Department> _DepartmentRepo;
		private readonly IWebHostEnvironment webHostEnvironment;

		private readonly UserManager<IdentityUser> userManager;



		public EmployeeController(ApplicationDbContext context, IBaseRepo<Employee> employeeRepo, IMapper mapper, IBaseRepo<Department> DepartmentRepo, IWebHostEnvironment hostEnvironment, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_EmployeeRepo = employeeRepo;
			_Mapper = mapper;
			_DepartmentRepo = DepartmentRepo;
			webHostEnvironment = hostEnvironment;
			this.userManager = userManager;
		}


		// GET: Employee
		public IActionResult Index()
		{
			try
			{
				var Emps = _EmployeeRepo.GetAll();
				var Depts = _DepartmentRepo.GetAll();
				var Model = _Mapper.Map<List<EmployeeDto>>(Emps);
				foreach (var employeeDto in Model)
				{
					employeeDto.ImageFileName = GetImageFilePath(employeeDto);
					employeeDto.DepartmentName = Depts.FirstOrDefault(x => x.Id == employeeDto.DepartmentId)?.Name;
				}
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
		public async Task<IActionResult> Create( EmployeeDto employeeDto)
		{
			try
			{
				if (ModelState.IsValid)
				{
					string uniqueFileName = UploadedFile(employeeDto);
					Employee employee = _Mapper.Map<Employee>(employeeDto);
					employee.ImageFile = uniqueFileName;
					employee.ImageFileName = uniqueFileName;
					employee.SetBasicData(CRUD_OperationType.Create);

					var user = new IdentityUser { UserName = employeeDto.UserName, Email = employeeDto.Email };
					var result = await userManager.CreateAsync(user, employeeDto.Password);
					employee.Department = _context.Departments.Find(employee.DepartmentId);
					_EmployeeRepo.Add(employee);
				}
			}
			catch (Exception ex)
			{
				var xx = ex.ToString();
			}

			return RedirectToAction("Index");
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
			var employees = _EmployeeRepo.GetAll();
			var departments = _DepartmentRepo.GetAll();
			EmployeeDto Model = _Mapper.Map<EmployeeDto>(employee);
			Model.ImageFileName=GetImageFilePath(Model);
			ViewBag.ManagerId = new SelectList(employees, "Id", "FirstName", Model.ManagerId);
			ViewBag.DepartmentId = new SelectList(departments, "Id", "Name", Model.DepartmentId);
			return View(Model);

		}


		// POST: Employee/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken] 
		public IActionResult Edit(int id, EmployeeDto employeeDto)
		{
			if (id != employeeDto.Id)
			{
				return NotFound();
			}


			try
			{
				if (ModelState.IsValid)
				{
					string uniqueFileName = UploadedFile(employeeDto);
					
					Employee employee = _Mapper.Map<Employee>(employeeDto);
					employee.ImageFile = uniqueFileName;
					employee.ImageFileName = uniqueFileName;
					employee.SetBasicData(CRUD_OperationType.Update);
					employee.Department = _context.Departments.Find(employee.DepartmentId);
					_EmployeeRepo.Update(employee);

				}
				else
				{

				}
			}
			catch (Exception)
			{
			}


			return RedirectToAction("Index");
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
		private string GetUniqueFileName(string fileName)
		{
			string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
			return uniqueFileName;
		}
		private string UploadedFile(EmployeeDto model)
		{
			string uniqueFileName = null;

			if (model.ImageFile != null)
			{
				string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					model.ImageFile.CopyTo(fileStream);
				}
			}
			return uniqueFileName;
		}
		private string GetImageFilePath(EmployeeDto employeeDto)
		{
			if (!string.IsNullOrEmpty(employeeDto.ImageFileName))
			{
				string imagePath = Path.Combine("/images", employeeDto.ImageFileName);
				return Url.Content(imagePath);
			}

			return null;
		}
		
	}
}
