namespace LampStore.Models.ViewModels
{
	public class SortViewModel
	{
		public SortCatalog NameSortAsc { get; private set; } // значение для сортировки по имени от а-я, a-z
		public SortCatalog NameSortDesc { get; private set; } // значение для сортировки по имени от я-а, z-a
		public SortCatalog PriceSortAsc { get; private set; }    // значение для сортировки по цене по возрастанию
		public SortCatalog PriceSortDesc { get; private set; }    // значение для сортировки по цене по убыванию
		public SortCatalog AvailabilitySort { get; private set; }   // значение для сортировки по наличию
		public SortCatalog NoveltiesSort { get; private set; }   // значение для сортировки по новизне
		public SortCatalog Current { get; private set; }     // текущее значение сортировки

		public SortViewModel(SortCatalog sortOrder)
		{
			NameSortAsc = sortOrder = SortCatalog.NameAsc;
			NameSortDesc = sortOrder = SortCatalog.NameDesc;
			PriceSortAsc = sortOrder = SortCatalog.PriceAsc;
			PriceSortDesc = sortOrder = SortCatalog.PriceDesc;
			NoveltiesSort = sortOrder = SortCatalog.Novelties;
			AvailabilitySort = sortOrder = SortCatalog.AvailabilitySt;
			Current = sortOrder;
		}
	}
}