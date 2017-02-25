namespace Angular2CoreBase.Common.Interfaces
{
	using System;

	public interface IWebServiceSettings
	{
		Uri BaseUri { get; }
		string ApiRoot { get; }
		double? Version { get; }
	}
}