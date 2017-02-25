namespace Angular2CoreBase.Common.Interfaces
{
	public interface ILogService
	{
		void LogMessage(string message);

		void LogError(string error);
	}
}
