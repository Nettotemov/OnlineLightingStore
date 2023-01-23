using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LampStore.Models
{
	public class Info
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название")]
		public string? NameInfo { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите информацию")]
		public string? Value { get; set; }
		public string Description { get; set; } = string.Empty;
	}
}