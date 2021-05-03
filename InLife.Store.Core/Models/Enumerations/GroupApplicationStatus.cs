using System;

namespace InLife.Store.Core.Models
{
	public sealed class GroupApplicationStatus : Enumeration<string>
	{
		public static GroupApplicationStatus Quote               = new GroupApplicationStatus("Quote", "Quote");
		public static GroupApplicationStatus Application         = new GroupApplicationStatus("Application", "Application");
		public static GroupApplicationStatus Payment             = new GroupApplicationStatus("Payment", "Payment");
		public static GroupApplicationStatus PaymentProof        = new GroupApplicationStatus("PaymentProof", "Pending Payment");
		public static GroupApplicationStatus PaymentConfirmation = new GroupApplicationStatus("PaymentConfirmation", "Payment Confirmation");
		public static GroupApplicationStatus Cancelled           = new GroupApplicationStatus("Cancelled", "Cancelled");
		public static GroupApplicationStatus Complete            = new GroupApplicationStatus("Complete", "Forwarded to InLife's Corporate Solution");

		public GroupApplicationStatus() { }

		private GroupApplicationStatus(string id, string name) : base(id, name) { }

		public static GroupApplicationStatus FromId(string id, GroupApplicationStatus defaultValue = null)
		{
			return Enumeration<string>.FromId<GroupApplicationStatus>(id, defaultValue);
		}

		public static GroupApplicationStatus FromName(string name, GroupApplicationStatus defaultValue = null)
		{
			return Enumeration<string>.FromName<GroupApplicationStatus>(name, defaultValue);
		}
	}
}
