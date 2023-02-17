using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LampStore.Services;

namespace LampStore.Models
{
	public class InfoProp
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название")]
		public string? Name { get; set; }
		public string? Value { get; set; }
		public string? InfoPropLink { get; set; }
		public int InfoId { get; set; }
		public TypesAdditionalFields TypesAddintionalFields { get; set; }
	}
}