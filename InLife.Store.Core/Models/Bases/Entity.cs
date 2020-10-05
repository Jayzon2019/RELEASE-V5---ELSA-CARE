using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InLife.Store.Core.Models
{
	public abstract class BaseEntity
	{

	}

	public abstract class Entity<TKey> : BaseEntity, IEntity<TKey>
	{
		public virtual TKey Id { get; set; }
	}
}
