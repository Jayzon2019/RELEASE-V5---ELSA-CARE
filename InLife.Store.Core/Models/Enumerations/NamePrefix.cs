using System;

namespace InLife.Store.Core.Models
{
	public sealed class NamePrefix : Enumeration<int>
	{
		public static NamePrefix Atty = new NamePrefix(19, "Atty");
		public static NamePrefix Dr   = new NamePrefix(20, "Dr");
		public static NamePrefix Dra  = new NamePrefix(21, "Dra");
		public static NamePrefix Engr = new NamePrefix(22, "Engr");
		public static NamePrefix Hon  = new NamePrefix(23, "Hon");
		public static NamePrefix Mr   = new NamePrefix(24, "Mr");
		public static NamePrefix Mrs  = new NamePrefix(25, "Mrs");
		public static NamePrefix Ms   = new NamePrefix(26, "Ms");
		public static NamePrefix Msgr = new NamePrefix(27, "Msgr");
		public static NamePrefix Rev  = new NamePrefix(28, "Rev");

		public NamePrefix() { }

		private NamePrefix(int id, string name) : base(id, name) { }

		public static NamePrefix FromId(int id)
		{
			return Enumeration<int>.FromId<NamePrefix>(id);
		}

		public static NamePrefix FromName(string name)
		{
			return Enumeration<int>.FromName<NamePrefix>(name);
		}
	}
}
