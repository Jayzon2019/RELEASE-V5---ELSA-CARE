using System;
using System.IO;
using System.Threading.Tasks;
using InLife.Store.Core.Models;

namespace InLife.Store.Core.Business
{
	public interface IPrimeSecureApplicationProcessing
	{
		PrimeSecureApplication GetApplication(string refcode);

		Task<PrimeSecureApplication> RequestQuote(PrimeSecureQuoteForm form);
		//Task<PrimeSecureApplication> UpdateQuote(string refcode, PrimeSecureQuoteForm form);
		//Task<PrimeSecureApplication> SaveApplication(string refcode, PrimeSecureApplicationForm form);
	}
}
