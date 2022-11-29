using System.ComponentModel.DataAnnotations;

namespace LampStore.Models
{
	public class Category
	{
		public long? ID { get; set; }
		[Required(ErrorMessage = "Пожалуйста, введите название Категории")]
		public string? CategoryName { get; set; }
		[Required(ErrorMessage = "Пожалуйста, укажите путь до изображения категории")]
		public string? CategoryImg { get; set; }
		[Required(ErrorMessage = "Пожалуйста, укажите описание категории")]
		public string Description { get; set; } = string.Empty;
		[Required(ErrorMessage = "Пожалуйста, укажите родительскую категорию")]
		public long? ParentID { get; set; }
	}
}