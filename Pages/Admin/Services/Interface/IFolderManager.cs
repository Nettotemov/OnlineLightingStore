namespace LampStore.Pages.Admin.Services.Interface
{
	internal interface IFolderManager
	{
		internal Task CeateDirectoryAsync(string directoryPath, long id);
		internal void DeleteDirectory(string directoryPath);
	}
}
