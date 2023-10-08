using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AribTask.Service
{
	public interface IBaseRepo<T> where T : class
	{
		T GetById(int Id);
		List<T> GetAll();
		void Add(T entity);
		void Update(T entity);
		int Delete(T entity);
		List<T> GetAllWithInclude(string[] includes = null);
		int GetLastCode(Expression<Func<T, object>> orderBy = null);
		//bool IsExistRecord(Expression<Func<T, bool>> condetion = null);
	}
}
