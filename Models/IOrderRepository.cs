namespace LampStore.Models
{
	public interface IOrderRepository
	{
		IQueryable<Order> Orders { get; }

		void CreateOrder(Order order);
		void SaveOrder(Order order);
	}
}