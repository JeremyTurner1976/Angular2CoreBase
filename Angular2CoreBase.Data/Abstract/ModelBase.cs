using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Angular2CoreBase.Data.Models;

namespace Angular2CoreBase.Data.Abstract
{
	public abstract class ModelBase
	{
		[Key]
		public int Id { get; protected set; }

	}
}

