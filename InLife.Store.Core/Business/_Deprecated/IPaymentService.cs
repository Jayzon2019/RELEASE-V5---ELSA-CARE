using InLife.Store.Core.Models.ContentEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InLife.Store.Core.Business
{
	public interface IPaymentService
	{
		PaymentStatus SavePaymentStatus(PaymentStatus paymentStatus);
	}
}
