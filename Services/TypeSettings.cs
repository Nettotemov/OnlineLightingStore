using System.ComponentModel.DataAnnotations;

namespace LampStore.Services
{
	public enum TypeSettings : byte
	{
		[Display(Name = "Другое")]
		Other = 0,

		[Display(Name = "Email")]
		Email = 1,

		[Display(Name = "Телефон")]
		PhoneNumber = 2,

		[Display(Name = "Социальная сеть")]
		Socials = 3,

		[Display(Name = "Адрес")]
		Address = 4,

		[Display(Name = "Текст для футера")]
		TextFooter = 5,

		[Display(Name = "Время работы")]
		WorkingHours = 6
	}
}