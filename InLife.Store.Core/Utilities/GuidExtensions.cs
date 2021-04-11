using System;
using Newtonsoft.Json.Linq;

namespace InLife.Store.Core.Utilities
{
	public static class GuidExtensions
	{
		public static string Shorten(this Guid guid)
		{
			return ShortGuid.Encode(guid);
		}

		public static string ToReferenceCode(this Guid guid)
		{
			return KeyGenerator.NewReferenceCode(guid);
		}
	}
}
