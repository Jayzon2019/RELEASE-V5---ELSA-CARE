using System;
using System.Text;
using System.Collections.Generic;

namespace InLife.Store.Core.Utilities
{
	/// <summary>
	/// </summary>
	public static class Base34
	{
		internal static readonly char[] charSetBase34 = {
			'A','B','C','D','E','F','G','H','I','J','K','M',
			'N','P','Q','R','S','T','U','V','W','X','Y','Z',
			'0','1','2','3','4','5','6','7','8','9'};

		public static string Encode(string value)
		{
			var byteArr = Encoding.UTF8.GetBytes(value);
			return Encode(byteArr);
		}

		public static string Encode(byte[] byteArr)
		{
			var converted = Convert(byteArr, 256, 34);
			var builder = new StringBuilder();

			for (var i = 0; i < converted.Length; i++)
				builder.Append(charSetBase34[converted[i]]);

			return builder.ToString();
		}

		public static string Decode(string base34, int padding = 0)
		{
			var byteArr = DecodeToByteArray(base34, padding);
			var length = byteArr.Length;

			return Encoding.UTF8.GetString(byteArr, 0, length);
		}

		public static byte[] DecodeToByteArray(string base34, int padding = 0)
		{
			var byteArr = new byte[base34.Length];

			for (var i = 0; i < byteArr.Length; i++)
				byteArr[i] = (byte)Array.IndexOf(charSetBase34, base34[i]);

			var converted = Convert(byteArr, 34, 256);
			var convertedLength = converted.Length;

			if (padding < 1)
				return converted;

			var padded = new byte[padding];
			var paddedLength = padded.Length;

			Array.Fill<byte>(padded, 0x00);
			converted.CopyTo(padded, paddedLength - convertedLength);

			return padded;
		}

		private static byte[] Convert(byte[] source, int sourceBase, int targetBase)
		{
			var result = new List<int>();
			int count;
			while ((count = source.Length) > 0)
			{
				var quotient = new List<byte>();
				int remainder = 0;
				for (var i = 0; i != count; i++)
				{
					int accumulator = source[i] + remainder * sourceBase;
					byte digit = System.Convert.ToByte((accumulator - (accumulator % targetBase)) / targetBase);
					remainder = accumulator % targetBase;
					if (quotient.Count > 0 || digit != 0)
					{
						quotient.Add(digit);
					}
				}

				result.Insert(0, remainder);
				source = quotient.ToArray();
			}

			var output = new byte[result.Count];
			for (int i = 0; i < result.Count; i++)
				output[i] = (byte)result[i];

			return output;
		}
	}
}
// glen@projectgrey.net //
