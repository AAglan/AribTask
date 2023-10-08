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

    public List<T> GetAllWithInclude(string[] includes = null)
    {
      IQueryable<T> query = _context.Set<T>();
      if (includes != null)
      {
        foreach (var item in includes)
        {
          query = query.Include(item);
        }
      }
      return query.ToList();

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
    public int GetLastCode(Expression<Func<T, object>> orderBy = null)
    {
      int LastCode = 0;
      IQueryable<T> query = _context.Set<T>();
      var LastRecord = query.OrderBy(orderBy).LastOrDefault();
      if (LastRecord != null)
      {
        LastCode = (int)LastRecord.GetType().GetProperty("Code").GetValue(LastRecord) + 1;
      }
      else
      {
        LastCode = 1;
      }
      return LastCode;
    }
    public bool IsExistRecord(Expression<Func<T, bool>> condetion = null)
    {
      bool IsExist = false;
      IQueryable<T> query = _context.Set<T>();
      var Record = query.Where(condetion).FirstOrDefault();
      if (Record != null)
      {
        IsExist = true;
      }
      return IsExist;
    }
  }
}
