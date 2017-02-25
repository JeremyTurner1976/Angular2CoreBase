namespace Angular2CoreBase.Common.Interfaces
{
	using System;
	using CommonEnums.FileService;

	public interface IFileService
	{
		bool SaveTextToDirectoryFile(DirectoryFolders directory, string strMessage);

		string[] LoadTextFromDirectoryFile(
			DirectoryFolders directory,
			string strFileName = "",
			DateTime dtIdentifier = new DateTime());

		bool DeleteOldFilesInFolder(DirectoryFolders directory, int nFilesToSave);

		bool DeleteFilesByDays(DirectoryFolders directory, int nDays);

		string GetDirectoryFolderLocation(DirectoryFolders directory);
	}
}