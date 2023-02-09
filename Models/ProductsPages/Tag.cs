using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace LampStore.Models
{
	public class Tag
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите значение")]
		public string Value { get; set; }

		[JsonIgnore]
		[BindNever]
		public ICollection<Product> Products { get; set; } = new List<Product>();

	}
}