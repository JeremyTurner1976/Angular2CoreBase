using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Data.Interfaces
{
	using Abstract;

	public interface ITrackedModelDecorator<T> where T : TrackedModelBase
	{
		T GetDecoratedModel(T trackedModel, int applicationUserId);
	}
}
