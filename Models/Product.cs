using System.ComponentModel.DataAnnotations.Schema;

namespace LampStore.Models
{
	public class Product
	{
		public long? ProductID { get; set; }
		public string Name { get; set; } = string.Empty;
		public string MainPhoto { get; set; } = string.Empty;
		public string Photos { get; set; } = string.Empty;

		[Column(TypeName = "bigint")]
		public long Price { get; set; }
		public string Tags { get; set; } = string.Empty;
		public string Color { get; set; } = string.Empty;
		public string Type { get; set; } = string.Empty;
		public string Kelvins { get; set; } = string.Empty;
		public string MinDescription { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public bool Availability { get; set; } //наличие
		public string Material { get; set; } = string.Empty;
		public long? CategoryId { get; set; }
		public long? ParentCategoryId { get; set; }
		public Category? Category { get; set; }

	}
}