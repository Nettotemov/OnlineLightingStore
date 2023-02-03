using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LampStore.Models
{
	public class Cooperation
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название")]
		public string? NameCooperation { get; set; }
		public string? CooperationImg { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите описание для привью")]
		public string? MinDescription { get; set; }
		public string Description { get; set; } = string.Empty;
		public bool IsVisible { get; set; }
	}
}