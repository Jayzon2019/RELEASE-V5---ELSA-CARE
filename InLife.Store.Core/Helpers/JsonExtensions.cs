using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InLife.Store.Core.Helpers
{
	public static class JsonExtensions
	{
		public static void Rename(this JToken token, string newName)
		{
			if (token == null)
				throw new InvalidOperationException("Cannot rename null token.");

			var parent = token.Parent;
			if (parent == null)
				throw new InvalidOperationException("The parent is missing.");

			var newToken = new JProperty(newName, token);
			parent.Replace(newToken);
		}

		public static string SearchValue(this JArray jarray, string searchName, string searchValue, string resultName)
		{
			string resultValue = "";

			//var items = jarray.SelectTokens("$.[?(@.Country=='India')]");
			return resultValue;
		}
	}
}
