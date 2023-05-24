using System.ComponentModel.DataAnnotations;

namespace LampStore.Models
{
	public class ConfidentPolicy
	{
		[Key]
		public int ID { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название")]
		public string Name { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;

	}
}