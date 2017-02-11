using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Angular2CoreBase.Data.Models;

namespace Angular2CoreBase.Data.Abstract
{
	public abstract class TrackedModelBase : ModelBase
	{
		public DateTime Created { get; set; }

		public int? CreatedByUserId { get; set; }

		[ForeignKey("CreatedByUserId")]
		public virtual ApplicationUser CreatedByUser { get; set; }

		public DateTime UpdatedDateTime { get; set; }

		public int? UpdatedByUserId { get; set; }

		[ForeignKey("UpdatedByUserId")]
		public virtual ApplicationUser UpdatedByUser { get; set; }
	}
}
