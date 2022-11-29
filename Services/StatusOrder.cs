using System.ComponentModel.DataAnnotations;

namespace LampStore.Services
{
	public enum StatusOrder : byte
	{
		[Display(Name = "Новый")]
		NewOrder = 0, //новый заказ

		[Display(Name = "Ожидает проверки")]
		OrderPendingVerification = 1, // заказ ожидает проверки

		[Display(Name = "Принят")]
		OrderAccepted = 2,  // заказ принят

		[Display(Name = "Заказ подтвержден")]
		OrderConfirmed = 3,    // заказ подтвержден

		[Display(Name = "Заказ оплачивается")]
		OrderPaid = 4,   // заказ оплачивается

		[Display(Name = "Заказ ожидает отгрузки")]
		OrderAwaitingShip = 5, //заказ ожидает отгрузки

		[Display(Name = "Заказ доставляется")]
		OrderDelivered = 6, //заказ доставляется

		[Display(Name = "Заказ выполнен")]
		OrderComplete = 7, //заказ выполнен

		[Display(Name = "Заказ отменен")]
		OrderCancel = 8, //заказ отменен

		[Display(Name = "Без статуса")]
		NoStatus = 9 //без статуса

	}
}