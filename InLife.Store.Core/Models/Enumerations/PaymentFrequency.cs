using System;

namespace InLife.Store.Core.Models
{
	public sealed class PaymentFrequency : Enumeration<string>
	{
		public static PaymentFrequency Monthly = new PaymentFrequency("Monthly", "Monthly");
		public static PaymentFrequency Annual  = new PaymentFrequency("Annual",  "Annual");

		public PaymentFrequency() { }

		private PaymentFrequency(string id, string name) : base(id, name) { }

		public static PaymentFrequency FromId(string id, PaymentFrequency defaultValue = null)
		{
			return Enumeration<string>.FromId<PaymentFrequency>(id, defaultValue);
		}

		public static PaymentFrequency FromName(string name, PaymentFrequency defaultValue = null)
		{
			return Enumeration<string>.FromName<PaymentFrequency>(name, defaultValue);
		}
	}
}
