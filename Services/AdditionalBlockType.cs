using System.ComponentModel.DataAnnotations;

namespace LampStore.Services
{
	public enum AdditionalBlockType : byte
	{
		[Display(Name = "Обычный")]
		DefaultBlock = 0,

		[Display(Name = "Слайдер")]
		Slider = 1,

		[Display(Name = "Видео")]
		VideoBlock = 2
	}
}