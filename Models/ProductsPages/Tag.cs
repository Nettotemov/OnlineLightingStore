using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LampStore.Models.ProductsPages
{
	public class Tag
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите значение")]
		public string Value { get; set; } = null!;

		[JsonIgnore]
		[BindNever]
		public ICollection<Product>? Products { get; set; }

	}
}