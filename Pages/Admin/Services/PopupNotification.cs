using LampStore.Pages.Admin.Services.Interface;

namespace LampStore.Pages.Admin.Services
{
	public class PopupNotification : IPopupNotification
	{
		private List<Notification> ListNotifications { get; set; } = new();

		public Task<List<Notification>> GetItems()
		{
			return Task.FromResult(ListNotifications);
		}

		public void AddItem(Notification item)
		{
			ListNotifications.Add(item);
		}
	}
}