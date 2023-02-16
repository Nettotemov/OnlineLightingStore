using LampStore.Pages.Admin.Services.Interface;

namespace LampStore.Pages.Admin.Services
{
	internal class FolderManager : IFolderManager
	{
		private string? ErrorMassage { get; set; }
		private string? NameDirectory { get; set; }

		async Task IFolderManager.CeateDirectoryAsync(string directoryPath, long id)
		{
			try
			{
				NameDirectory = $"wwwroot/{directoryPath}{id.ToString()}";
				if (Directory.Exists(NameDirectory))
				{
					ErrorMassage = "Такая папка существует";
					return;
				}
				await Task.Run(() => Directory.CreateDirectory(NameDirectory));
				ErrorMassage = $"Каталог был успешно создан в {NameDirectory}. {Directory.GetCreationTime(NameDirectory)}";

				await CeateSubdirectoryAsync(NameDirectory);

			}
			catch (Exception e)
			{
				ErrorMassage = $"The process failed: {e.ToString()}";
			}
		}

		private async Task CeateSubdirectoryAsync(string path)
		{
			string[] subFolders = { "average", "webp" };

			foreach (var folder in subFolders)
			{
				string subFolderPath = Path.Combine(path, folder);
				if (!Directory.Exists(subFolderPath))
				{
					await Task.Run(() => Directory.CreateDirectory(subFolderPath));
					ErrorMassage = subFolderPath + "Созадана";

					string subWebpPath = Path.Combine(subFolderPath, "webp");
					if (!Directory.Exists(subWebpPath) && folder == "average")
					{
						await Task.Run(() => Directory.CreateDirectory(subWebpPath));
						ErrorMassage = $"{subWebpPath} в {subFolderPath} Созадана";
					}
				}
			}
		}

		void IFolderManager.DeleteDirectory(string directoryPath)
		{
			try
			{
				Directory.Delete(directoryPath, true);
			}
			catch (DirectoryNotFoundException ex)
			{
				Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
			}
			catch (UnauthorizedAccessException ex)
			{
				Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Произошла ошибка: " + ex.Message);
			}
		}
	}
}