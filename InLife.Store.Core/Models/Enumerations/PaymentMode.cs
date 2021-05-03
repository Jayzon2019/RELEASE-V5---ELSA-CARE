using System;

namespace InLife.Store.Core.Models
{
	public sealed class PaymentMode : Enumeration<string>
	{
		public static PaymentMode BankTransfer   = new PaymentMode("BankTransfer", "Bank Transfer");
		public static PaymentMode OverTheCounter = new PaymentMode("OTC", "Over-the-Counter");

		public PaymentMode() { }

		private PaymentMode(string id, string name) : base(id, name) { }

		public static PaymentMode FromId(string id, PaymentMode defaultValue = null)
		{
			return Enumeration<string>.FromId<PaymentMode>(id, defaultValue);
		}

		public static PaymentMode FromName(string name, PaymentMode defaultValue = null)
		{
			return Enumeration<string>.FromName<PaymentMode>(name, defaultValue);
		}
	}
}
