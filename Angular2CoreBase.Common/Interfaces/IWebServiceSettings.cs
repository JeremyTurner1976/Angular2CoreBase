using System;

namespace Angular2CoreBase.Common.Interfaces
{
    public interface IWebServiceSettings
    {
		Uri BaseUri { get; }
		string ApiRoot { get; }
		double? Version { get; }
	}
}
