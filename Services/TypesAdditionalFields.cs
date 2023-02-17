using System.ComponentModel.DataAnnotations;

namespace LampStore.Services
{
	public enum TypesAdditionalFields : byte
	{
		[Display(Name = "Текст")]
		Text = 0,

		[Display(Name = "Строка")]
		TextString = 1,

		[Display(Name = "URL")]
		URL = 2,

		[Display(Name = "Изображение")]
		IMG = 3,

		[Display(Name = "HTML-тег")]
		HTML = 4,

		[Display(Name = "Поумолчание")]
		Default = 5,

		[Display(Name = "Заголовок")]
		Caption = 6
	}
}