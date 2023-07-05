using System.ComponentModel.DataAnnotations;
using LampStore.Models.CollectionsLights;
using LampStore.Models.ProductsPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LampStore.Models.LightsModels
{
	public class ModelLight
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите значение")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "Пожалуйста, введите описание")]
		public string Description { get; set; } = null!;
		public string Img { get; set; } = null!;
		public bool IsAvailable { get; set; }
		public bool IsHomePage { get; set; }

		[BindNever]
		public ICollection<AdditionalBlocksForModelLight>? AdditionalBlocks { get; init; }

		[BindNever]
		public ICollection<Product>? Products { get; init; }
		
		[Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, укажите коллекцию")]
		public int CollectionLightId { get; set; }
		public CollectionLight CollectionModel { get; set; } = null!;

	}
}