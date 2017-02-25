using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Data.Decorators
{
	using Abstract;
	using Interfaces;
	using Models;

	public class TrackedModelDecorator<T>: ITrackedModelDecorator<T> where T : TrackedModelBase
	{
		public T GetDecoratedModel(T trackedModel, int applicationUserId)
		{
			bool isNew = trackedModel.Id > 0;

			if (trackedModel.CreatedByUser == null)
			{
				trackedModel.CreatedByUserId = applicationUserId;
				trackedModel.CreatedDateTime = DateTime.UtcNow;
			}
			else 
			{
				trackedModel.UpdatedByUserId = applicationUserId;
				trackedModel.UpdatedDateTime = DateTime.UtcNow;
			}

			return trackedModel;
		}
	}
}
