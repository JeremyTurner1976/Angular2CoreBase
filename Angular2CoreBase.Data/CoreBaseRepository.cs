using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular2CoreBase.Data.Abstract;
using Angular2CoreBase.Data.Database;
using Angular2CoreBase.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Angular2CoreBase.Data
{
	//http://deviq.com/repository-pattern/
	public class CoreBaseRepository<T> : IRepository<T> where T : ModelBase
	{
		private readonly CoreBaseContext coreBaseContext;

		public CoreBaseRepository(CoreBaseContext coreBaseContext)
		{
			this.coreBaseContext = coreBaseContext;
		}

		public virtual T GetById(int id)
		{
			return coreBaseContext
				.Set<T>()
				.Find(id);
		}

		public virtual IEnumerable<T> List()
		{
			return coreBaseContext
				.Set<T>()
				.AsEnumerable();
		}

		public virtual IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
		{
			return coreBaseContext
				.Set<T>()
				.Where(predicate)
				.AsEnumerable();
		}
		public int Count()
		{
			return coreBaseContext
				.Set<T>()
				.AsEnumerable()
				.Count();
		}

		public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
		{
			return coreBaseContext
				.Set<T>()
				.Where(predicate)
				.AsEnumerable()
				.Count();
		}

		public void Add(T model)
		{
			coreBaseContext
				.Set<T>()
				.Add(model);


			Save(model);
		}

		public void Update(T model)
		{
			coreBaseContext
				.Entry(model)
				.State = EntityState.Modified;


			Save(model);
		}

		public void Delete(T model)
		{
			coreBaseContext
				.Set<T>()
				.Remove(model);

			Save(model);
		}

		public void Save(T model)
		{
			coreBaseContext
				.SaveChanges();
		}
	}
}
