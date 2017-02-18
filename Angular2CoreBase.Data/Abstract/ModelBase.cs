using System.ComponentModel.DataAnnotations;

namespace Angular2CoreBase.Data.Abstract
{
	public abstract class ModelBase
	{
		[Key]
		public int Id { get; protected set; }
	}
}

