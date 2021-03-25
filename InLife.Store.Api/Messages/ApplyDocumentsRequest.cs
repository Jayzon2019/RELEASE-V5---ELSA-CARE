using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLife.Store.Api.Messages
{
	public class ApplyDocumentsRequest
	{
		public Guid ApplyDocumentsId { get; set; }
		public string SECRegistration { get; set; }
		public string EmployeeCesusForm { get; set; }
		public string EntityPlanForm { get; set; }
		public string AuthRepresentativeID { get; set; }
		public string BIRNoticeForm { get; set; }
		public string IncorporationArticles { get; set; }
		public string IdentityCertificate { get; set; }
		public string PostPolicyForm { get; set; }
		public bool IsCheckDataPrivacy { get; set; }
		public bool IsCheckUNSCR { get; set; }
		public bool IsCheckDeclarationStatement { get; set; }
		public bool IsCheckSubmittedPhlippinesApp { get; set; }
		public bool IsCheckInLifeProducts { get; set; }
	}
}
