using System;
using System.ComponentModel.DataAnnotations.Schema;
using Angular2CoreBase.Data.Models;

namespace Angular2CoreBase.Data.Abstract
{
	public abstract class TrackedModelBase : ModelBase
	{
		public DateTime CreatedDateTime { get; set; }

		public int? CreatedByUserId { get; set; }

		[ForeignKey("CreatedByUserId")]
		public virtual ApplicationUser CreatedByUser { get; set; }

		public DateTime UpdatedDateTime { get; set; }

		public int? UpdatedByUserId { get; set; }

		[ForeignKey("UpdatedByUserId")]
		public virtual ApplicationUser UpdatedByUser { get; set; }
	}
}
