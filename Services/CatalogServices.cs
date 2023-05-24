using LampStore.Models.ProductsPages;

namespace LampStore.Services
{
	public class CatalogServices
	{
		public IList<Product> ProductsByName(IList<Product> products, string name)
		{
			return products.Where(p => p.Name.ToUpper().StartsWith(name.ToUpper()) || p.Artikul.ToUpper().StartsWith(name.ToUpper())).ToList();
		}
		public IList<Product> ProductsByTags(IList<Product> products, string[] tags)
		{
			return products.Where(p => tags.All(t => p.ProductTags.Select(s => s.Value).Any(tag => tag.Contains(t)))).ToList();
		}

		public IList<Product> ProductsByColors(IList<Product> products, string[] colors)
		{
			return products.Where(p => colors.Any(color => p.Color.Contains(color))).ToList();
		}

		public IList<Product> ProductsByTypes(IList<Product> products, string[] types)
		{
			return products.Where(p => types.Any(type => p.ProductType!.Name.Contains(type))).ToList();
		}
		public IList<Product> ProductsByMaterials(IList<Product> products, string[] materials)
		{
			return products.Where(p => materials.All(material => p.Material.Contains(material))).ToList();
		}

		public IList<Product> ProductsByCategory(IList<Product> products, string category)
		{
			return products.Where(p => p.Category!.CategoryName == category).ToList();
		}

		public IList<Product> ProductsUpMaxPrice(IList<Product> products, string maxPrice)
		{
			return products.Where(p => p.Price <= Convert.ToInt64(maxPrice)).ToList();
		}

		public IList<Product> ProductsUpMinPrice(IList<Product> products, string minPrice)
		{
			return products.Where(p => p.Price >= Convert.ToInt64(minPrice)).ToList();
		}

		public IList<Product> ProductsFromMinToMaxPrice(IList<Product> products, string minPrice, string maxPrice)
		{
			return products.Where(p => p.Price >= Convert.ToInt64(minPrice)).Where(p => p.Price <= Convert.ToInt64(maxPrice)).ToList();
		}

		public List<Product> FilterForLightSource(string lightSource, List<Product> products)
		{
			return products.Where(p => p.LightSource == lightSource).ToList();
		}

		public List<Product> FilterForColor(string color, List<Product> products)
		{
			return products.Where(p => p.Color == color).ToList();
		}

		public List<Product> FilterForSize(string size, List<Product> products)
		{
			return products.Where(p => p.Size == size).ToList();
		}
		public List<Product> FilterForModel(string model, List<Product> products)
		{
			return products.Where(p => p.ModelLight?.Name == model).ToList();
		}

		public List<Product> FilterForPower(string powerW, List<Product> products)
		{
			return products.Where(p => p.PowerW == powerW).ToList();
		}

		public List<Product> FilterForAddController(string dim, List<Product> products)
		{
			return products.Where(p => p.AddControl == dim).ToList();
		}
	}
}