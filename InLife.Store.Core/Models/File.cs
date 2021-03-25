using System;
using System.Collections.Generic;

namespace InLife.Store.Core.Models
{
	public class File : Entity<Guid>
	{
		public virtual MediaType MediaType
		{
			get { return MediaType.FromId(MediaTypeId); }
			set { MediaTypeId = value.Id; }
		}
		public string MediaTypeId { get; set; }

		public string FileName { get; set; }

		public string Data { get; set; }
	}
}
