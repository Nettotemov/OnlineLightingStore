using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LampStore.Models.CollectionsLights;
using LampStore.Models.LightsModels;
using LampStore.Models.MetaTags;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace LampStore.Models.ProductsPages
{
	public class Product
	{
		public long Id { get; init; }

		[Required(ErrorMessage = "Пожалуйста, введите артикул")]
		public string Artikul { get; set; } = null!;

		[Required(ErrorMessage = "Пожалуйста, введите название продукта")]
		public string Name { get; set; } = null!;
		
		public string MainPhoto { get; set; } = "imegs/system-img/no-img.png";
		
		public string? Photos { get; set; }

		[Required(ErrorMessage = "Пожалуйста, введите положительную цену")]
		[Column(TypeName = "bigint")]
		public long Price { get; set; }
		
		[Required(ErrorMessage = "Пожалуйста, введите основной цвет товара")]
		public string Color { get; set; } = null!;
		
		[Required(ErrorMessage = "Пожалуйста, введите цветовую температуру")]
		public string Kelvins { get; set; } = null!;
		
		[Required(ErrorMessage = "Пожалуйста, введите короткое описание продукта")]
		public string MinDescription { get; set; } = null!;

		[Required(ErrorMessage = "Пожалуйста, введите описание продукта")]
		public string Description { get; set; } = null!;

		[Required(ErrorMessage = "Пожалуйста, укажите есть ли в наличии")]
		public bool Availability { get; set; }
		
		[Required(ErrorMessage = "Пожалуйста, укажите материал")]
		public string Material { get; set; } = null!;

		[Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, укажите категорию")]
		public long CategoryId { get; set; }
		public Category Category { get; init; } = null!;
		public int? Discount { get; set; }

		[Column(TypeName = "bigint")]
		public long? OldPrice { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите размер изделия")]
		public string Size { get; set; } = null!;
		
		[Required(ErrorMessage = "Пожалуйста, укажите размер базы")]
		public string BaseSize { get; set; } = null!;
		public int? CordLength { get; set; }

		[Required(ErrorMessage = "Пожалуйста, укажите источник света (Например LED)")]
		public string LightSource { get; set; } = null!;
		
		[Required(ErrorMessage = "Пожалуйста, укажите вольтажность")]
		public string PowerW { get; set; } = null!;

		[JsonIgnore]
		[BindNever]
		public ICollection<Tag>? ProductTags { get; init; }

		[Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, укажите тип")]
		public int ProductTypeId { get; set; }
		public ProductType ProductType { get; init; } = null!;
		public int? CollectionLightId { get; set; }
		public CollectionLight? CollectionLight { get; set; }
		public int? ModelLightId { get; set; }

		[JsonIgnore]
		public ModelLight? ModelLight { get; set; }
		
		[Required(ErrorMessage = "Пожалуйста, укажите управление")]
		public string AddControl { get; set; } = null!;
		public bool IsPublished { get; set; }
		public MetaData MetaData { get; set; } = null!;
	}
}