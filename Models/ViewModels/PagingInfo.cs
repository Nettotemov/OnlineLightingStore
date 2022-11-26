namespace LampStore.Models.ViewModels
{
	public class PagingInfo
	{
		public int TotalItems { get; set; } //Всего продуктов
		public int ItemsPerPage { get; set; } //Количество элементов на странице
		public int CurrentPage { get; set; } //Текущая страница
		public int TotalPages { get; set; } //всего страниц
		public long? PlaceholderMaxPrice { get; set; } //Плейцхолдер макимального прайса
		public long? PlaceholderMinPrice { get; set; } //Плейцхолдер минимального прайса

	}
}
