namespace LampStore.Pages.Admin.Services.Interface
{
	public interface IPopupNotification
	{
		Task<List<Notification>> GetItems();
		void AddItem(Notification item);
	}
}
