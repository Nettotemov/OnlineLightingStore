using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace LampStore.Models.ProductsPages
{
	public class ProductType
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите название")]
		public string Name { get; set; } = null!;

		[JsonIgnore]
		[BindNever]
		public ICollection<Product>? Products { get; set; }

	}
}