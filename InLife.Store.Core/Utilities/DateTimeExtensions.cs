using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace InLife.Store.Core.Utilities
{
	public static class DateTimeExtensions
	{
		public static DateTime ToLocalDateTime(this DateTimeOffset value)
		{
			var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();

			string[] localTimeZones = { "Asia/Manila", "Asia/Singapore", "Singapore Standard Time" };

			foreach (var id in localTimeZones)
			{
				if (systemTimeZones.Any(x => x.Id == id))
				{
					var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById(id);
					return TimeZoneInfo.ConvertTimeFromUtc(value.DateTime, localTimeZone);
				}
			}

			return value.DateTime;
		}
	}
}
