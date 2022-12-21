namespace LampStore.Models
{
	public class Cart
	{
		public List<CartLine> Lines { get; set; } = new List<CartLine>();

		public virtual void AddItem(Product product, int quantity) //добавление в корзину
		{
			CartLine? line = Lines.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
			if (line == null)
			{
				Lines.Add(new CartLine
				{
					Product = product,
					Quantity = quantity
				});
			}
			else
			{
				line.Quantity += quantity;
			}
		}

		public virtual void RemoveLine(Product product) => Lines.RemoveAll(l => l.Product.ProductID == product.ProductID); //Удаление товара из корзины

		public decimal ComputeTotalValue() => Lines.Sum(e => e.Product.Price * e.Quantity); //Согалсование корзины
		public virtual void Clear() => Lines.Clear(); //очистка корзины

		public virtual void Recalculation(Product product, int quantity)
		{
			CartLine? line = Lines.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();

			if (line != null)
			{
				line.Quantity = quantity;
			}
		}
	}

	public class CartLine
	{
		public int CartLineID { get; set; }
		public Product Product { get; set; } = new();
		public int Quantity { get; set; }
	}
}