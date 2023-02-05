using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LampStore.Models
{
	public class Info
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название")]
		public string? NameInfo { get; set; }

		[MaxLength(60)]
		[Required(ErrorMessage = "Пожалуйста, укажите текст для баннера")]
		public string? TextForBanner { get; set; }
		public string Value { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string? SvgUrl { get; set; } = string.Empty;
		public bool IsAvailable { get; set; }

		[BindNever]
		public ICollection<InfoProp> InfoProp { get; set; } = new List<InfoProp>();
	}
}