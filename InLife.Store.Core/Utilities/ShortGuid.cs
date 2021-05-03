using System;

namespace InLife.Store.Core.Utilities
{
	/// <summary>
	/// Converts GUID to Base62 string to allow for shorter and friendlier URL codes, and smaller DB storage.
	/// When creating a DB field, set it as VARCHAR(22)
	/// </summary>
	public static class ShortGuid
	{
		//public ShortGuid(Guid guid)
		//{

		//}

		public static string NewShortGuid()
		{
			var guid = Guid.NewGuid();

			return Encode(guid);
		}

		public static string Encode(Guid guid)
		{
			var byteArr = guid.ToByteArray();

			return Base62.Encode(byteArr);
		}

		public static Guid ToGuid(string value)
		{
			var byteArr = Base62.DecodeToByteArray(value, 16);

			return new Guid(byteArr);
		}
	}
}
// glen@projectgrey.net //
