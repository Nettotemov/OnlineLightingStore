namespace LampStore.Pages.Admin.Services
{
	public class Notification
	{
		public int CountNotification { get; set; }
		public string HeaderNotification { get; set; }
		public string BodyNotification { get; set; }
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