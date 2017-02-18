using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2CoreBase.Common.Interfaces
{
    public interface IWebServiceSettings
    {
		Uri BaseUri { get; }
		string ApiRoot { get; }
		double? Version { get; }
	}
}
