using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.CommonModels
{
	public class Email
	{
		public List<string> ToAddresses { get; set;  }
		public List<string> CarbonCopies { get; set;  }
		public List<string> BackupCarbonCopies { get; set;  }
		public string Message { get; set;  }
		public string Body { get; set; }
	}
}
