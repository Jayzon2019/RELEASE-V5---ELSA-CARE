using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InLife.Store.Identity.Utilities
{
	public static class Utilities
	{
		private const string ABFROM = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789&<>";
		private const string ABTO = "NOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLM~@*";

		public static string Encode(string s)
		{
			//var s = "activatedPreregisteredMeterId=14";

			if (string.IsNullOrEmpty(s))
				return "";

			char[] abtoArray = ABTO.ToCharArray();
			char[] chars = s.ToCharArray();

			var result = "";

			foreach (char c in chars)
			{
				var charIndex = ABFROM.IndexOf(c);
				result += (charIndex < 0) ? c.ToString() : abtoArray[charIndex].ToString();
			}

			return result;
		}

		public static string Decode(string s)
		{
			//var s = "activatedPreregisteredMeterId=14";

			if (string.IsNullOrEmpty(s))
				return "";

			char[] abtoArray = ABTO.ToCharArray();
			char[] chars = s.ToCharArray();

			var result = "";

			foreach (char c in chars)
			{
				var charIndex = ABTO.IndexOf(c);
				result += (charIndex < 0) ? c.ToString() : ABFROM[charIndex].ToString();
			}

			return result;
		}
	}
}
