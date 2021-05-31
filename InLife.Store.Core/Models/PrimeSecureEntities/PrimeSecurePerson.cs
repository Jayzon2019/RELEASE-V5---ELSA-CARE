using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class PrimeSecurePerson : Entity<Guid>
	{
		public DateTimeOffset CreatedDate { get; set; }

		// Personal Information
		public string NamePrefix { get; set; }
		public string NameSuffix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }

		public string Nationality { get; set; }
		public string CivilStatus { get; set; }
		public string Gender { get; set; }
		public DateTime? BirthDate { get; set; }

		// Contact Information
		public string EmailAddress { get; set; }
		public string MobileNumber { get; set; }

		// Addresses
		public Guid? BirthAddressId { get; set; }
		public virtual PrimeSecureAddress BirthAddress { get; set; }

		public Guid? HomeAddressId { get; set; }
		public virtual PrimeSecureAddress HomeAddress { get; set; }

		public Guid? WorkAddressId { get; set; }
		public virtual PrimeSecureAddress WorkAddress { get; set; }


		public virtual ICollection<PrimeSecureApplication> Applications { get; set; }
	}
}
