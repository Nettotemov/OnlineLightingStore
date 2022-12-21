using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LampStore.Models
{
	public class Product
	{
		public long? ProductID { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название продукта")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Пожалуйста, укажите путь до изображения")]
		public string MainPhoto { get; set; } = string.Empty;

		[Required(ErrorMessage = "Пожалуйста, укажите путь до всех изображений")]
		public string Photos { get; set; } = string.Empty;

		[Required(ErrorMessage = "Пожалуйста, введите положительную цену")]
		[Column(TypeName = "bigint")]
		public long Price { get; set; }
		
		[Required(ErrorMessage = "Пожалуйста, укажите теги относящиеся к продукту")]
		public string Tags { get; set; } = string.Empty;
		public string Color { get; set; } = string.Empty;

		[Required(ErrorMessage = "Пожалуйста, укажите тип продукта")]
		public string Type { get; set; } = string.Empty;
		public string Kelvins { get; set; } = string.Empty;
		public string MinDescription { get; set; } = string.Empty;

		[Required(ErrorMessage = "Пожалуйста, введите описание продукта")]
		public string Description { get; set; } = string.Empty;

		[Required(ErrorMessage = "Пожалуйста, укажите есть ли в наличии")]
		public bool Availability { get; set; } //наличие
		public string Material { get; set; } = string.Empty;

		[Required(ErrorMessage = "Пожалуйста, укажите ID категории")]
		public long? CategoryId { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите ID родительской категории")]
		public long? ParentCategoryId { get; set; }
		public Category? Category { get; set; }

		public int? Discount { get; set; } = null;
		
		[Column(TypeName = "bigint")]
		public long? OldPrice { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите размер изделия")]
		public string Size { get; set; } = string.Empty;

		public string BaseSize { get; set; } = string.Empty;
		public int? CordLength { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите источник света (Например LED)")]
		public string LightSource { get; set; } = string.Empty;
		
		public string PowerW { get; set; } = string.Empty;
		
	}
}