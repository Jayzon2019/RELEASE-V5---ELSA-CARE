using System;

namespace InLife.Store.Core.Models
{
	public sealed class IncomeSource : Enumeration<int>
	{
		public static IncomeSource Family     = new IncomeSource(121, "Family Income");
		public static IncomeSource Employment = new IncomeSource(122, "Employment");
		public static IncomeSource Savings    = new IncomeSource(123, "Savings");
		public static IncomeSource Rentals    = new IncomeSource(210, "Rentals");
		public static IncomeSource Interests  = new IncomeSource(212, "Interests");
		public static IncomeSource Investment = new IncomeSource(213, "Investment");
		public static IncomeSource Remittance = new IncomeSource(214, "Remittance");
		public static IncomeSource Pension    = new IncomeSource(215, "Pension");
		public static IncomeSource Others     = new IncomeSource(124, "Others");

		public IncomeSource() { }

		private IncomeSource(int id, string name) : base(id, name) { }

		public static IncomeSource FromId(int id)
		{
			return Enumeration<int>.FromId<IncomeSource>(id);
		}

		public static IncomeSource FromName(string name)
		{
			return Enumeration<int>.FromName<IncomeSource>(name);
		}
	}
}
