using System;

namespace InLife.Store.Core.Models
{
	public sealed class Relationship : Enumeration<int>
	{
		public static Relationship CommonLawSpouse = new Relationship(1, "Common-Law-Spouse");
		public static Relationship Spouse          = new Relationship(2, "Spouse");
		public static Relationship SpouseSpouse    = new Relationship(3, "Spouse");

		public static Relationship Son           = new Relationship(4, "Son");
		public static Relationship Daughter      = new Relationship(5, "Daughter");
		public static Relationship Grandson      = new Relationship(6, "Grandson");
		public static Relationship Granddaughter = new Relationship(7, "Granddaughter");

		public static Relationship Dependent         = new Relationship(8,  "Dependent");
		public static Relationship AdoptedChild      = new Relationship(9,  "Adopted Child");
		public static Relationship IllegitimateChild = new Relationship(10, "Illegitimate Child");

		public static Relationship Grandfather    = new Relationship(11, "Grandfather");
		public static Relationship Grandmother    = new Relationship(12, "Grandmother");
		public static Relationship Father         = new Relationship(13, "Father");
		public static Relationship Mother         = new Relationship(14, "Mother");
		public static Relationship AdoptiveParent = new Relationship(15, "Adoptive Parent");
		public static Relationship Guardian       = new Relationship(16, "Guardian/Custodian");

		public static Relationship Uncle = new Relationship(17, "Uncle");
		public static Relationship Aunt  = new Relationship(18, "Aunt");

		public static Relationship Brother = new Relationship(19, "Brother");
		public static Relationship Sister  = new Relationship(20, "Sister");

		public static Relationship Nephew = new Relationship(21, "Nephew");
		public static Relationship Niece  = new Relationship(22, "Niece");

		public static Relationship Company         = new Relationship(23, "Company");
		public static Relationship Employer        = new Relationship(24, "Employer");
		public static Relationship Employee        = new Relationship(25, "Employee");
		public static Relationship BusinessPartner = new Relationship(26, "Business Partner");
		public static Relationship Estate          = new Relationship(27, "Estate");
		public static Relationship Others          = new Relationship(28, "Others");

		public Relationship() { }

		private Relationship(int id, string name) : base(id, name) { }

		public static Relationship FromId(int id)
		{
			return Enumeration<int>.FromId<Relationship>(id);
		}

		public static Relationship FromName(string name)
		{
			return Enumeration<int>.FromName<Relationship>(name);
		}
	}
}
