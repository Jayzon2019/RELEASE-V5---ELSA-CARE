using System;

namespace InLife.Store.Core.Models
{
	public sealed class NameSuffix : Enumeration<int>
	{
		public static NameSuffix II  = new NameSuffix(10, "II");
		public static NameSuffix III = new NameSuffix(11, "III");
		public static NameSuffix IV  = new NameSuffix(12, "IV");
		public static NameSuffix Jr  = new NameSuffix(13, "Jr");
		public static NameSuffix Md  = new NameSuffix(14, "Md");
		public static NameSuffix Op  = new NameSuffix(15, "Op");
		public static NameSuffix Phd = new NameSuffix(16, "Phd");
		public static NameSuffix Sr  = new NameSuffix(17, "Sr");
		public static NameSuffix V   = new NameSuffix(18, "V");

		public NameSuffix() { }

		private NameSuffix(int id, string name) : base(id, name) { }

		public static NameSuffix FromId(int id)
		{
			return Enumeration<int>.FromId<NameSuffix>(id);
		}

		public static NameSuffix FromName(string name)
		{
			return Enumeration<int>.FromName<NameSuffix>(name);
		}
	}
}
