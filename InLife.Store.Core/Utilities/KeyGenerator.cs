using System;
using Nanoid;
using Newtonsoft.Json;

namespace InLife.Store.Core.Utilities
{
	public static class KeyGenerator
	{
		private const string charSet = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";

		public static string NewReferenceCode()
		{
			return Nanoid.Nanoid.Generate(charSet, 10);
		}

		public static string NewReferenceCode(Guid? guid = null)
		{
			if (!guid.HasValue)
				guid = Guid.NewGuid();

			var byteArr = guid.Value.ToByteArray();
			var year = DateTime.Now.ToString("yyyy");

			return year + Base62.Encode(byteArr);
		}

		//private static string Encode(Guid guid)
		//{
		//	var byteArr = guid.ToByteArray();

		//	return Base34.Encode(byteArr);
		//	return Base62.Encode(byteArr);
		//}

		//private static Guid ToGuid(string value)
		//{
		//	var byteArr = Base34.DecodeToByteArray(value, 16);

		//	return new Guid(byteArr);
		//}
	}
}
// glen@projectgrey.net //
