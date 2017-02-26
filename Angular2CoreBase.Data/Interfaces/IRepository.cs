namespace Angular2CoreBase.Data.Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using Abstract;
	using Common.Interfaces;

	public interface IRepository<T> where T : ICommonModel
	{
		T GetById(int id);
		IEnumerable<T> List();
		IEnumerable<T> List(Expression<Func<T, bool>> predicate);
		int Count();
		int Count(Expression<Func<T, bool>> predicate);
		void Add(T model);
		void Delete(T model);
		void Update(T model);
		void SaveChanges();
	}
}