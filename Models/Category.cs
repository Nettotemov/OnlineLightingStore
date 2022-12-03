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

		public string? HeadingTwo{ get; set; } = string.Empty;
		public string? ImgTwoUrl { get; set; } = string.Empty;
		public string? DescriptionTwo { get; set; } = string.Empty;

		public string? HeadingThree { get; set; } = string.Empty;
		public string? ImgThreeUrl { get; set; } = string.Empty;
		public string? DescriptionThree { get; set; } = string.Empty;

		public string? Slider { get; set; } = string.Empty;
		public bool DisplaySlider { get; set; }

		public bool DisplayHomePage { get; set; }
	}
}