using System;
using System.Collections.Generic;
using System.Linq;
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
	}
}
