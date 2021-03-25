using System;

namespace InLife.Store.Core.Models
{
	public sealed class MediaType : Enumeration<string>
	{
		public static MediaType JPG  = new MediaType("image/jpeg", "jpg", "JPEG");
		public static MediaType PNG  = new MediaType("image/png", "png", "PNG");
		public static MediaType PDF  = new MediaType("application/pdf", "pdf", "Adobe PDF");

		public static MediaType XLS  = new MediaType("application/vnd.ms-excel", "xls", "Microsoft Excel");
		public static MediaType XLSX = new MediaType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "xlsx", "Microsoft Excel (OpenXML)");

		public static MediaType DOC  = new MediaType("application/msword", "doc", "Microsoft Word");
		public static MediaType DOCX = new MediaType("application/vnd.openxmlformats-officedocument.wordprocessingml.document", "docx", "Microsoft Word");

		public static MediaType CSV = new MediaType("text/csv", "csv", "Comma-Separated Values (CSV)");

		public static MediaType ZIP  = new MediaType("application/zip", "zip", "ZIP Archive");
		public static MediaType GZIP = new MediaType("application/gzip", "gz", "GZip Compressed Archive");
		public static MediaType SevenZIP = new MediaType("application/x-7z-compressed", "7z", "GZip Compressed Archive");
		public static MediaType RAR = new MediaType("application/vnd.rar", "rar", "RAR Archive");


		public MediaType() { }

		private MediaType(string id, string ext, string name) : base(id, name) { Extension = ext; }

		public static MediaType FromId(string id)
		{
			return Enumeration<string>.FromId<MediaType>(id);
		}

		public static MediaType FromName(string name)
		{
			return Enumeration<string>.FromName<MediaType>(name);
		}

		public string Extension { get; }
	}
}
