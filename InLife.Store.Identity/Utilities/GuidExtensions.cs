using System;
using Newtonsoft.Json.Linq;

namespace InLife.Store.Identity.Utilities
{
	public static class GuidExtensions
	{
		public static string Shorten(this Guid guid)
		{
			return ShortGuid.Encode(guid);
		}
	}
}
