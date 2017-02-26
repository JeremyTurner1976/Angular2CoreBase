using System.ComponentModel.DataAnnotations;

namespace Angular2CoreBase.Data.Abstract
{
	using Common.Interfaces;
	public abstract class ModelBase : ICommonModel
	{
		[Key]
		public int Id { get; protected set; }
	}
}

