namespace Angular2CoreBase.Common.Services.LoggingServices
{
	using System.Text;
	using CommonEnums.FileService;

	using Interfaces;

	public class FileLoggingService : ILogService
	{
		private readonly IFileService _fileService;

		public FileLoggingService(IFileService fileService)
		{
			_fileService = fileService;
		}

		public bool LogMessage(string strLogMessage)
		{
			var sb = new StringBuilder();
			sb.AppendLine(strLogMessage);

			_fileService.SaveTextToDirectoryFile(DirectoryFolders.Logs, sb.ToString());
			return true;
		}
	}
}
