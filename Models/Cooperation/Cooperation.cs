using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LampStore.Models
{
	public class Cooperation
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название")]
		public string? NameCooperation { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите путь до изображения категории")]
		public string? CategoryImg { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите описание")]
		public string Description { get; set; } = string.Empty;
		public bool IsVisible { get; set; }
		public string? HeadingTwo { get; set; } = string.Empty;
		public string? ImgTwoUrl { get; set; } = string.Empty;
		public string? DescriptionTwo { get; set; } = string.Empty;
	}
}