using LampStore.Pages.Admin.Services.Interface;

namespace LampStore.Pages.Admin.Services
{
	internal class FolderManager : IFolderManager
	{
		private string? ErrorMassage { get; set; }
		private string? NameDirectory { get; set; }

		void IFolderManager.CeateDirectory(string directoryPath, long id)
		{
			try
			{
				NameDirectory = $"wwwroot/{directoryPath}{id.ToString()}";
				if (Directory.Exists(NameDirectory))
				{
					ErrorMassage = "Такая папка существует";
					return;
				}

				DirectoryInfo di = Directory.CreateDirectory(NameDirectory);
				ErrorMassage = $"Каталог был успешно создан в {NameDirectory}. {Directory.GetCreationTime(NameDirectory)}";

			}
			catch (Exception e)
			{
				ErrorMassage = $"The process failed: {e.ToString()}";
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