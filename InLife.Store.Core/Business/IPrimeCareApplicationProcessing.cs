using System;
using System.IO;
using System.Threading.Tasks;
using InLife.Store.Core.Models;

namespace InLife.Store.Core.Business
{
	public interface IPrimeCareApplicationProcessing
	{
		PrimeCareApplication GetPrimeCareApplication(Guid id);
		PrimeCareApplication GetPrimeCareApplication(string refcode);

		Task<PrimeCareApplication> RequestQuote(PrimeCareQuoteForm form);
		//Task<PrimeCareApplication> UpdateQuote(string refcode, PrimeCareQuoteForm form);
	}
}
