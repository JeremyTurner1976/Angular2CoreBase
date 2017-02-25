using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
