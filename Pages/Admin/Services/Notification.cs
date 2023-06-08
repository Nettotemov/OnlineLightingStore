namespace LampStore.Pages.Admin.Services
{
	public class Notification
	{
		public int CountNotification { get; set; }
		public string HeaderNotification { get; set; } = string.Empty;
		public string BodyNotification { get; set; } = string.Empty;
		public string? NameNotification { get; set; }

		public Notification(int countNotification, string headerNotification, string bodyNotification, string? nameNotification)
		{
			CountNotification = countNotification;
			HeaderNotification = headerNotification;
			BodyNotification = bodyNotification;
			NameNotification = nameNotification;
		}
	}
}