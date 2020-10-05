using System;

namespace InLife.Store.Core.Models
{
	public sealed class Gender : Enumeration<int>
	{
		public static Gender Male    = new Gender(8, "Male");
		public static Gender Female  = new Gender(7, "Female");
		public static Gender Unknown = new Gender(9, "Unknown");

		public Gender() { }

		private Gender(int id, string name) : base(id, name) { }

		public static Gender FromId(int id)
		{
			return Enumeration<int>.FromId<Gender>(id);
		}

		public static Gender FromName(string name)
		{
			return Enumeration<int>.FromName<Gender>(name);
		}
	}
}
