using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class Customer : Entity<int>
	{
		// Personal Information
		public string NamePrefix { get; set; }        //public int NamePrefixId { get; set; }
		public string NameSuffix { get; set; }        //public int NameSuffixId { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }

		public string Nationality { get; set; }       //public int NationalityId { get; set; }
		public string CivilStatus { get; set; }       //public int CivilStatusId { get; set; }
		public string Gender { get; set; }            //public int GenderId { get; set; }

		// Birth Information
		public DateTime? BirthDate { get; set; }
		public string BirthCountry { get; set; }
		public string BirthRegion { get; set; }
		public string BirthCity { get; set; }

		// Contact Information
		public string EmailAddress { get; set; }
		public string MobileNumber { get; set; }

		// Addresses
		public int? HomeAddressId { get; set; }
		public virtual Address HomeAddress { get; set; }
		public int? WorkAddressId { get; set; }
		public virtual Address WorkAddress { get; set; }
		//public string PreferredMailingAddress { get; set; }

		// Work and Income
		//public string  CompanyName { get; set; }
		//public string  Occupation { get; set; }            //public int OccupationId { get; set; }
		//public decimal IncomeMonthlyAmount { get; set; }
		//public string  IncomeSource { get; set; }          //public int IncomeSourceId { get; set; }

		// Identity Documents
		//public virtual IdentityDocument IdentityDocument { get; set; }
		//public virtual IdentityDocument SecondaryIdentityDocument { get; set; }

		public virtual ICollection<Quote> Quotes { get; set; }
	}
}
