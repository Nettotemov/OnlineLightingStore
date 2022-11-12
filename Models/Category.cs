namespace LampStore.Models
{
	public class Category
	{
		public long? ID { get; set; }
		public string? CategoryName { get; set; }
		public string? CategoryImg { get; set; }
		public string Description { get; set; } = string.Empty;
		public long? ParentID { get; set; }
	}
}