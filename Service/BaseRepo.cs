using AribTask.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AribTask.Service
{
	public class BaseRepo<T> : IBaseRepo<T> where T : class
  {
    protected ApplicationDbContext _context;
    public BaseRepo(ApplicationDbContext context)
    {
      _context = context;
    }
    public void Add(T entity)
    {
      _context.Set<T>().Add(entity);
      _context.SaveChanges();
    }

      public int Delete(T entity)
    {
      _context.Set<T>().Remove(entity);
      return _context.SaveChanges();

    }

    public List<T> GetAll()
    {
      return _context.Set<T>().ToList();

    }

    public T GetById(int Id)
    {
      return _context.Set<T>().Find(Id);
    }

   
    public void Update(T entity)
    {
      _context.Set<T>().Update(entity);
      _context.SaveChanges();

    }
 

  }
}
