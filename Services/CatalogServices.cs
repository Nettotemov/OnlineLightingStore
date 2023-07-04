using LampStore.Models.ProductsPages;

namespace LampStore.Services
{
	public static class CatalogServices
	{
		public static IList<Product> ProductsByName(IList<Product> products, string name)
		{
			return products.Where(p => p.Name.ToUpper().StartsWith(name.ToUpper()) || p.Artikul.ToUpper().StartsWith(name.ToUpper())).ToList();
		}
		
		public static IList<Product> ProductsByTags(IList<Product> products, string[] tags)
		{
			return products.Where(p => tags.All(t => p.ProductTags != null && p.ProductTags.Select(s => s.Value).Any(tag => tag.Contains(t)))).ToList();
		}

		public static IList<Product> ProductsByColors(IList<Product> products, string[] colors)
		{
			return products.Where(p => colors.Any(color => p.Color.Contains(color))).ToList();
		}

		public static IList<Product> ProductsByTypes(IList<Product> products, string[] types)
		{
			return products.Where(p => types.Any(type => p.ProductType.Name.Contains(type))).ToList();
		}
		
		public static IList<Product> ProductsByMaterials(IList<Product> products, string[] materials)
		{
			return products.Where(p => materials.All(material => p.Material.Contains(material))).ToList();
		}

		public static IList<Product> ProductsByCategory(IList<Product> products, string category)
		{
			return products.Where(p => p.Category.CategoryName == category).ToList();
		}

		public static IList<Product> ProductsUpMaxPrice(IList<Product> products, string maxPrice)
		{
			return products.Where(p => p.Price <= Convert.ToInt64(maxPrice)).ToList();
		}

		public static IList<Product> ProductsUpMinPrice(IList<Product> products, string minPrice)
		{
			return products.Where(p => p.Price >= Convert.ToInt64(minPrice)).ToList();
		}

		public static IList<Product> ProductsFromMinToMaxPrice(IList<Product> products, string minPrice, string maxPrice)
		{
			return products.Where(p => p.Price >= Convert.ToInt64(minPrice)).Where(p => p.Price <= Convert.ToInt64(maxPrice)).ToList();
		}

		public static IList<Product> FilterForLightSource(string lightSource, IList<Product> products)
		{
			return products.Where(p => p.LightSource == lightSource).ToList();
		}

		public static IList<Product> FilterForColor(string color, IList<Product> products)
		{
			return products.Where(p => p.Color == color).ToList();
		}

		public static IList<Product> FilterForSize(string size, IList<Product> products)
		{
			return products.Where(p => p.Size == size).ToList();
		}
		public static IList<Product> FilterForModel(string model, IList<Product> products)
		{
			return products.Where(p => p.ModelLight?.Name == model).ToList();
		}

		public static IList<Product> FilterForPower(string powerW, IList<Product> products)
		{
			return products.Where(p => p.PowerW == powerW).ToList();
		}

		public static IList<Product> FilterForAddController(string dim, IList<Product> products)
		{
			return products.Where(p => p.AddControl == dim).ToList();
		}
	}
}