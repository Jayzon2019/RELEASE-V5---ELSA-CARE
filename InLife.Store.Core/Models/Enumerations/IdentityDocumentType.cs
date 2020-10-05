using System;

namespace InLife.Store.Core.Models
{
	public sealed class IdentityDocumentType : Enumeration<string>
	{
		#region

		public static IdentityDocumentType Passport        = new IdentityDocumentType("Passport",          "Passport");
		public static IdentityDocumentType DriversLicense  = new IdentityDocumentType("Driver's License",  "Driver's License");
		public static IdentityDocumentType UMID            = new IdentityDocumentType("UMID",              "Unified Multipurpose ID (UMID)");
		public static IdentityDocumentType PRC             = new IdentityDocumentType("PRC",               "Professional Regulation Commission(PRC) ID");
		public static IdentityDocumentType ComelecID       = new IdentityDocumentType("COMELEC",           "Voter’s Or COMELEC ID");
		public static IdentityDocumentType SSS             = new IdentityDocumentType("SSS",               "Social Security System(SSS) ID");
		public static IdentityDocumentType TIN             = new IdentityDocumentType("TIN",               "Tax Identification Number (TIN) Card");
		public static IdentityDocumentType ACR             = new IdentityDocumentType("ACR",               "Alien Certificate Of Registration (ACR) Or Immigrant Certificate Of Registration");
		public static IdentityDocumentType Postal          = new IdentityDocumentType("Postal",            "Postal ID (Issued 2015 Onwards)");
		public static IdentityDocumentType Philsys         = new IdentityDocumentType("Philsys",           "Philsys ID");
		public static IdentityDocumentType School          = new IdentityDocumentType("School",            "School ID");
		public static IdentityDocumentType SeniorCitizen   = new IdentityDocumentType("Senior Citizen",    "Senior Citizen ID");
		public static IdentityDocumentType HDMF            = new IdentityDocumentType("HDMF",              "Pagibig Or Home Development Mutual Fund (HDMF) ID");
		public static IdentityDocumentType GovernemtOffice = new IdentityDocumentType("Government Office", "Government Office ID Or GOCC ID");
		public static IdentityDocumentType OWWA            = new IdentityDocumentType("OWWA",              "Overseas Worker’s Welfare Administration(OWWA) ID");
		public static IdentityDocumentType OFW             = new IdentityDocumentType("OFW",               "Overseas Filipino Worker(OFW) ID");
		public static IdentityDocumentType PhilHealth      = new IdentityDocumentType("PhilHealh",         "PhilHealth ID");

		#endregion

		public IdentityDocumentType() { }

		private IdentityDocumentType(string id, string name) : base(id, name) { }

		public static IdentityDocumentType FromId(string id)
		{
			return Enumeration<string>.FromId<IdentityDocumentType>(id);
		}

		public static IdentityDocumentType FromName(string name)
		{
			return Enumeration<string>.FromName<IdentityDocumentType>(name);
		}
	}
}
