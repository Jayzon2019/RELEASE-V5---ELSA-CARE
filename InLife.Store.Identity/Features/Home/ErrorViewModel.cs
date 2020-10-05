using IdentityServer4.Models;

namespace InLife.Store.Identity.Features
{
	public class ErrorViewModel
	{
		public ErrorViewModel()
		{
		}

		public ErrorViewModel(string error)
		{
			Error = new ErrorMessage { Error = error };
		}

		public ErrorMessage Error { get; set; }
	}
}