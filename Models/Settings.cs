using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LampStore.Services;

namespace LampStore.Models
{
	public class Settings
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название")]
		public string? NameSettings { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите информацию")]
		public string? Value { get; set; }
		public bool IsVisible { get; set; }
		public TypeSettings Setting { get; set; }
	}
}