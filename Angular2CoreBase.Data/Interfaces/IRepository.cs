using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Angular2CoreBase.Data.Abstract;

namespace Angular2CoreBase.Data.Interfaces
{
	public interface IRepository<T> where T : ModelBase
	{
		T GetById(int id);
		IEnumerable<T> List();
		IEnumerable<T> List(Expression<Func<T, bool>> predicate);
		int Count();
		int Count(Expression<Func<T, bool>> predicate);
		void Add(T model);
		void Delete(T model);
		void Update(T model);
	}
}
