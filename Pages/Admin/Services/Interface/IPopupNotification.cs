namespace LampStore.Pages.Admin.Services.Interface
{
	public interface IPopupNotification
	{
		IList<Notification> ListNotifications { get; set; }
		void AddItem(Notification notification);
		void CreateNotification(string title, string description, string name, int count = 1);
	}
}
