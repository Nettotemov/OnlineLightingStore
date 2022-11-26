using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.ComponentModel.DataAnnotations;

public enum SortCatalog
{
	[Display(Name = "По наличию")]
	AvailabilitySt = 0, //по наличию

	[Display(Name = "Сначала дешевые")]
	PriceAsc = 1, // по цене по возрастанию

	[Display(Name = "Сначала дорогие")]
	PriceDesc = 2,    // по цене по убыванию

	[Display(Name = "По названию А-Я")]
	NameAsc = 3,    // по имени по возрастанию

	[Display(Name = "По названию Я-А")]
	NameDesc = 4,   // по имени по убыванию

	[Display(Name = "Новинки")]
	Novelties = 5 //по новизне

}