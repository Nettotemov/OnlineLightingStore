using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LampStore.Models
{
	public class Order
	{
		[BindNever]
		public int OrderID { get; set; }

		[BindNever]
		public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

		[Required(ErrorMessage = "Пожалуйста, введите: Имя")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите: Телефон")]
		public string? Phone { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите: Email")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите: Адрес")]
		public string? Line1 { get; set; }
		public string? Line2 { get; set; }
		public string? Line3 { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название: Города")]
		public string? City { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите: Индекс")]
		public string? Zip { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название: Страны")]
		public string? Country { get; set; }
		public bool GiftWrap { get; set; } = true;

		[BindNever]
		public bool Shipped { get; set; }
	}
}