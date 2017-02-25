namespace Angular2CoreBase.Data.Decorators
{
	using System;
	using Abstract;
	using Interfaces;

	public class TrackedModelDecorator<T> : ITrackedModelDecorator<T> where T : TrackedModelBase
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