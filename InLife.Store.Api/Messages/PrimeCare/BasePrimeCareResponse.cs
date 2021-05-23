using System;
using InLife.Store.Core.Models;

namespace InLife.Store.Api.Messages
{
	public class BasePrimeCareResponse
	{
		public BasePrimeCareResponse()
		{
		}

		public BasePrimeCareResponse(PrimeCareApplication model)
		{
			ReferenceCode = model.ReferenceCode;
			Status = model.Status;
		}

		public string ReferenceCode { get; set; }

		public string Status { get; set; }
	}
}
