namespace Angular2CoreBase.Common.Interfaces
{
	public interface IEmailService
	{
		void SendMail(
			string to,
			string carbonCopy,
			string backupCarbonCopy,
			string subject,
			string message);
	}
}