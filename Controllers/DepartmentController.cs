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
using Microsoft.AspNetCore.Authorization;

namespace AribTask.Controllers
{
	[Authorize(Roles = "Admin")]
	public class DepartmentController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IBaseRepo<Department> _DepartmentRepo;
		private readonly IMapper _Mapper;
		public DepartmentController(ApplicationDbContext context, IBaseRepo<Department> departmentRepo, IMapper mapper)
		{
			_context = context;
			_DepartmentRepo = departmentRepo;
			_Mapper = mapper;
		}

		// GET: Department
		public IActionResult Index()
		{
			try
			{
				var Department = _DepartmentRepo.GetAll();
				var Model = _Mapper.Map<List<DepartmentDto>>(Department);

				return View(Model);
			}
			catch (Exception)
			{
				return View();
			}
		}

		// GET: Department/Details/5
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}



			Department Department = _DepartmentRepo.GetById(id.Value);
			DepartmentDto Model = _Mapper.Map<DepartmentDto>(Department);
			return View(Model);
		}

		// GET: Department/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Department/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,DepartmentCost,EmployeesCount,ManagerId,Id,CreationDate,UpdatedDate,CreationUserId,UpdatedUserId")] DepartmentDto departmentDto)
		{
			try

			{
				if (ModelState.IsValid)
				{
					

						Department Department = _Mapper.Map<Department>(departmentDto);

						Department.SetBasicData(CRUD_OperationType.Create);

						_DepartmentRepo.Add(Department);

				
				}

			}

			catch (Exception)
			{
			}

			return View(departmentDto);
		}

		// GET: Department/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Department Department = _DepartmentRepo.GetById(id.Value);
			if (Department == null)
			{
				return NotFound();
			}
			DepartmentDto Model = _Mapper.Map<DepartmentDto>(Department);
			return View(Model);

		}

		// POST: Department/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, [Bind("Name,DepartmentCost,EmployeesCount,ManagerId,Id,CreationDate,UpdatedDate,CreationUserId,UpdatedUserId")] DepartmentDto departmentDto)
		{
			if (id != departmentDto.Id)
			{
				return NotFound();
			}


			try
			{
				if (ModelState.IsValid)
				{
					Department Department = _Mapper.Map<Department>(departmentDto);
					Department.SetBasicData(CRUD_OperationType.Update);
					_DepartmentRepo.Update(Department);

				}
				else
				{

				}
			}
			catch (Exception)
			{
			}


			return View(departmentDto);
		}

		// GET: Department/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Department Department = _DepartmentRepo.GetById(id.Value);
			if (Department == null)
			{
				return NotFound();
			}

			DepartmentDto model = _Mapper.Map<DepartmentDto>(Department);

			return View(model);
		}

		// POST: Department/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var Department = _DepartmentRepo.GetById(id);
			_DepartmentRepo.Delete(Department);

			return RedirectToAction(nameof(Index));
		}

		//private bool DepartmentDtoExists(int id)
		//{
		//	return _context.DepartmentDto.Any(e => e.Id == id);
		//}
	}
}
