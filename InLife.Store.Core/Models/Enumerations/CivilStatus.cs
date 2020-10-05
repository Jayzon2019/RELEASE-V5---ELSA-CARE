using System;

namespace InLife.Store.Core.Models
{
	public sealed class CivilStatus : Enumeration<int>
	{
		public static CivilStatus Annulled         = new CivilStatus(1,  "Annulled");
		public static CivilStatus Divorced         = new CivilStatus(2,  "Divorced");
		public static CivilStatus LegallySeparated = new CivilStatus(3,  "Legally Separated");
		public static CivilStatus Married          = new CivilStatus(4,  "Married");
		public static CivilStatus Single           = new CivilStatus(5,  "Single");
		public static CivilStatus Widowed          = new CivilStatus(6,  "Widowed");
		public static CivilStatus NotApplicable    = new CivilStatus(48, "Not Applicable");

		public CivilStatus() { }

		private CivilStatus(int id, string name) : base(id, name) { }

		public static CivilStatus FromId(int id)
		{
			return Enumeration<int>.FromId<CivilStatus>(id);
		}

		public static CivilStatus FromName(string name)
		{
			return Enumeration<int>.FromName<CivilStatus>(name);
		}
	}
}
