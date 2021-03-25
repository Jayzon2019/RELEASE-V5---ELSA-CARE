using System;
using System.ComponentModel.DataAnnotations;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class GroupPaymentRequest
	{
		[Required]
		[StringLength(20)]
		public string PaymentMethod { get; set; }
	}
}
