using System;
using InLife.Store.Core.Models.Enumerations;

namespace InLife.Store.Core.Models
{
	public class PrimeCareFile : File
	{
		public virtual PrimeCareApplication Application { get; set; }
	}
}
