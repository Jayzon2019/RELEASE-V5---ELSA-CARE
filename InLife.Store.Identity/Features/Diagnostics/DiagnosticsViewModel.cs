using System.Text;
using System.Collections.Generic;

using Microsoft.AspNetCore.Authentication;

using IdentityModel;
using Newtonsoft.Json;

namespace InLife.Store.Identity.Features
{
	public class DiagnosticsViewModel
	{
		public DiagnosticsViewModel(AuthenticateResult result)
		{
			AuthenticateResult = result;

			if (result.Properties.Items.ContainsKey("client_list"))
			{
				var encoded = result.Properties.Items["client_list"];
				var bytes = Base64Url.Decode(encoded);
				var value = Encoding.UTF8.GetString(bytes);

				Clients = JsonConvert.DeserializeObject<string[]>(value);
			}
		}

		public AuthenticateResult AuthenticateResult { get; }
		public IEnumerable<string> Clients { get; } = new List<string>();
	}
}