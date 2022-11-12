using LampStore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.ComponentModel.DataAnnotations;

public enum SortCatalog
{
	[Display(Name = "По названию А-Я")]
	NameAsc = 0,    // по имени по возрастанию

	[Display(Name = "По названию Я-А")]
	NameDesc = 1,   // по имени по убыванию

	[Display(Name = "Сначала дешевые")]
	PriceAsc = 2, // по цене по возрастанию

	[Display(Name = "Сначала дорогие")]
	PriceDesc = 3,    // по цене по убыванию

	[Display(Name = "По наличию")]
	AvailabilitySt = 4, //по наличию

	[Display(Name = "Новинки")]
	Novelties = 5 //по новизне

}