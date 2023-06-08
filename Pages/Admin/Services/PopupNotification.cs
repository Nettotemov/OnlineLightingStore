using LampStore.Pages.Admin.Services.Interface;

namespace LampStore.Pages.Admin.Services
{
	public class PopupNotification : IPopupNotification
	{
		public IList<Notification> ListNotifications { get; set; } = new List<Notification>();
		public void AddItem(Notification notification)
		{
			ListNotifications.Add(notification);
		}

		public void CreateNotification(string title, string description, string name, int count)
		{
			Notification notification = new(count, title, description, name);
			AddItem(notification);
		}
	}
}