using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace LampStore.Models
{
	public class ProductType
	{
		[Key]
		public int ID { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите значение")]
		public string Name { get; set; } = string.Empty;

		[JsonIgnore]
		[BindNever]
		public ICollection<Product> Products { get; set; } = new List<Product>();

	}
}