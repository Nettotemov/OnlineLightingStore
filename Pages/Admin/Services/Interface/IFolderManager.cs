namespace LampStore.Pages.Admin.Services.Interface
{
	internal interface IFolderManager
	{
		internal void CeateDirectory(string directoryPath, long id);

		internal void DeleteDirectory(string directoryPath);
	}
}
