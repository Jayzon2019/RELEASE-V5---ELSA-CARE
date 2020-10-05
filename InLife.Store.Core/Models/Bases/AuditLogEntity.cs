using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace InLife.Store.Core.Models
{
    public abstract class AuditLogEntity<TData, TKey> : AuditableEntity<System.Int64>, IAuditableEntity
	{
		public virtual TData Source { get; set; }
		public TKey SourceId { get; set; }

		public virtual TData OldData
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_OldData))
					return default(TData);

				return JsonConvert.DeserializeObject<TData>
				(
					_OldData,
					new JsonSerializerSettings
					{
						Error = delegate (object sender, ErrorEventArgs args)
						{
							var message = args.ErrorContext.Error.Message;
							args.ErrorContext.Handled = true;
						}
					}
				);
			}
			set { _OldData = JsonConvert.SerializeObject(value); }
		}
		public virtual string _OldData { get; set; }

		public virtual TData Data
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_Data))
					return default(TData);

				return JsonConvert.DeserializeObject<TData>
				(
					_Data,
					new JsonSerializerSettings
					{
						Error = delegate (object sender, ErrorEventArgs args)
						{
							var message = args.ErrorContext.Error.Message;
							args.ErrorContext.Handled = true;
						}
					}
				);
			}
			set { _Data = JsonConvert.SerializeObject(value); }
		}
		public virtual string _Data { get; set; }
	}
}
