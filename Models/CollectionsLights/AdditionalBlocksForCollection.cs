using System.ComponentModel.DataAnnotations;
using LampStore.Services;

namespace LampStore.Models.CollectionsLights
{
	public class AdditionalBlocksForCollection
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите название")]
		public string Caption { get; set; } = null!;

		[Required(ErrorMessage = "Пожалуйста, введите описание")]
		public string Description { get; set; } = null!;
		public bool IsAvailable { get; set; }
		public string? Url { get; set; }
		public AdditionalBlockType AdditionalBlockType { get; set; }
		public int CollectionLightId { get; set; }
		public CollectionLight CollectionModel { get; set; } = null!;
	}
}