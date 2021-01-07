using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InLife.Store.Core.Models
{
	public class BaseEntity
	{
		public virtual object Id { get; set; }
	}

	public abstract class Entity<TKey> : BaseEntity, IEntity<TKey>
	{
		public virtual new TKey Id { get; set; }
	}
}
