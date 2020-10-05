using System;
namespace InLife.Store.Core.Models.Enumerations
{
	public sealed class YesNoBoolean : Enumeration<bool>
	{
		public static YesNoBoolean Yes = new YesNoBoolean(true, "Yes");
		public static YesNoBoolean No  = new YesNoBoolean(false, "No");

		public YesNoBoolean() { }

		private YesNoBoolean(bool id, string name) : base(id, name) { }

		public static YesNoBoolean FromId(bool id)
		{
			return Enumeration<bool>.FromId<YesNoBoolean>(id);
		}

		public static YesNoBoolean FromName(string name)
		{
			return Enumeration<bool>.FromName<YesNoBoolean>(name);
		}
	}
}
