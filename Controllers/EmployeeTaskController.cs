﻿using System;
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
	public class EmployeeTaskController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IBaseRepo<EmployeeTask> _EmployeeTaskRepo;
		private readonly IMapper _Mapper;

		public EmployeeTaskController(ApplicationDbContext context, IBaseRepo<EmployeeTask> employeeTaskRepo, IMapper mapper)
		{
			_context = context;
			_EmployeeTaskRepo = employeeTaskRepo;
			_Mapper = mapper;
		}

		// GET: EmployeeTask
		public IActionResult Index()
		{
			try
			{
				var EmpTasks = _EmployeeTaskRepo.GetAll();
				var Model = _Mapper.Map<List<EmployeeTaskDto>>(EmpTasks);
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
			return View();
		}

		// POST: EmployeeTask/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Subject,Body,ManagerId,EmployeeId,Status,Id,CreationDate,UpdatedDate,CreationUserId,UpdatedUserId")] EmployeeTaskDto employeeTaskDto)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (!_EmployeeTaskRepo.IsExistRecord(b => b.Code == employeeTaskDto.Code))
					{
						EmployeeTask employeeTask = _Mapper.Map<EmployeeTask>(employeeTaskDto);
						employeeTask.SetBasicData(CRUD_OperationType.Create);

						_EmployeeTaskRepo.Add(employeeTask);

				}
				}

			}
			catch (Exception)
			{
			}

			return View(employeeTaskDto);
		}

		// GET: EmployeeTask/Edit/5
		public IActionResult Edit(int? id)
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

					_EmployeeTaskRepo.Update(employeeTask);

				}
				else
				{

				}
			}
			catch (Exception ex)
			{
			}


			return View(employeeTaskDto);
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
