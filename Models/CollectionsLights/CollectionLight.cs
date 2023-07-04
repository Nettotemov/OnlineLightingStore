using System.ComponentModel.DataAnnotations;
using LampStore.Models.LightsModels;
using LampStore.Models.MetaTags;
using LampStore.Models.ProductsPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LampStore.Models.CollectionsLights
{
	public class CollectionLight
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название")]
		public string Name { get; set; } = null!;

		[Required(ErrorMessage = "Пожалуйста, введите описание")]
		public string Description { get; set; } = null!;
		public string? Img { get; set; }
		public bool IsAvailable { get; set; }
		public bool IsHomePage { get; set; }
		
		public MetaData MetaData { get; set; } = null!;

		[BindNever]
		public ICollection<AdditionalBlocksForCollection>? AdditionalBlocks { get; init; }

		[BindNever]
		public ICollection<Product>? Products { get; init; }
		
		[BindNever]
		public ICollection<ModelLight>? ModelLights { get; init; }


	}
}