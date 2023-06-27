using System.ComponentModel.DataAnnotations;
using LampStore.Models.MetaTags;

namespace LampStore.Models
{
	public class Category
	{
		public long Id { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название Категории")]
		public string CategoryName { get; set; } = null!;
		public string CategoryImg { get; set; } = "imegs/system-img/no-img.png";

		[Required(ErrorMessage = "Пожалуйста, укажите описание категории")]
		public string Description { get; set; } = string.Empty;
		
		public long? ParentId { get; set; }

		public string? HeadingTwo { get; set; } = string.Empty;
		public string? ImgTwoUrl { get; set; } = string.Empty;
		public string? DescriptionTwo { get; set; } = string.Empty;

		public string? HeadingThree { get; set; } = string.Empty;
		public string? ImgThreeUrl { get; set; } = string.Empty;
		public string? DescriptionThree { get; set; } = string.Empty;

		public string? Slider { get; set; } = string.Empty;
		public bool DisplaySlider { get; set; }
		public bool DisplayHomePage { get; set; }
		public bool IsPublished { get; set; }
		public MetaData MetaData { get; set; } = null!;
	}
}