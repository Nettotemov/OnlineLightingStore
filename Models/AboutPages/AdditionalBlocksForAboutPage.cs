using System.ComponentModel.DataAnnotations;
using LampStore.Services;

namespace LampStore.Models.AboutPages
{
	public class AdditionalBlocksForAboutPage
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите заголовок")]
		public string Caption { get; set; } = null!;
		
		public string Img { get; set; } = "imegs/system-img/no-img.png";

		[Required(ErrorMessage = "Пожалуйста, введите описание")]
		public string Description { get; set; } = null!;
		
		public bool IsAvailable { get; set; }
		
		public AdditionalBlockType AdditionalBlockType { get; set; }
		
		public long AboutPageId { get; set; }
		public AboutPage AboutPage { get; set; } = null!;
	}
}