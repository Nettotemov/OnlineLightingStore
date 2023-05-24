using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using LampStore.Models.ProductsPages;

namespace LampStore.Models
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