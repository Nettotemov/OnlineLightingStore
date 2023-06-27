using System.ComponentModel.DataAnnotations;
using LampStore.Models.MetaTags;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LampStore.Models.AboutPages
{
	public class AboutPage
	{
		public long Id { get; set; }
		public string ImgOneUrl { get; set; } = "imegs/system-img/no-img.png";
		
		[Required(ErrorMessage = "Пожалуйста, введите заголовок")]
		public string Heading { get; set; } = null!;
		
		[Required(ErrorMessage = "Пожалуйста, введите параграф")]
		public string Paragraph { get; set; } = null!;
		
		[BindNever]
		public ICollection<AdditionalBlocksForAboutPage>? AdditionalBlocks { get; init; }
		
		public bool DisplayHomePage { get; set; }
		
		public bool IsPublished { get; set; }
		
		public bool MainAboutCompany { get; set; }
		
		public MetaData MetaData { get; set; } = null!;
	}
}