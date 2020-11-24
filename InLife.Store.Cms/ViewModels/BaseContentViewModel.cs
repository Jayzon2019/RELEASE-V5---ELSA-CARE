//TODO: USE AUTOMAPPER

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;


namespace InLife.Store.Cms.ViewModels
{
	public class BaseContentViewModel
	{
		public BaseContentViewModel()
		{
		}

		public BaseContentViewModel(BaseContentEntity model)
		{
			this.Id = model.Id;

			this.CreatedBy = model.CreatedBy?.Id.ToString();
			this.CreatedByName = $"{model.CreatedBy?.FirstName} {model.CreatedBy?.LastName}".Trim();
			this.CreatedDate = model.CreatedDate.ToOffset(new TimeSpan(8, 0, 0)).ToString("yyyy-MM-dd hh:mm tt");

			this.UpdatedBy = model.UpdatedBy?.Id.ToString();
			this.UpdatedByName = $"{model.UpdatedBy?.FirstName} {model.UpdatedBy?.LastName}".Trim();
			this.UpdatedDate = model.UpdatedDate?.ToOffset(new TimeSpan(8, 0, 0)).ToString("yyyy-MM-dd hh:mm tt");
		}

		public int Id { get; set; }

		public string CreatedDate { get; set; }

		public string CreatedBy { get; set; }

		public string CreatedByName { get; set; }

		public string UpdatedDate { get; set; }

		public string UpdatedBy { get; set; }

		public string UpdatedByName { get; set; }



		// This is a hack, old uploaded images doesn't have an image data
		// TODO: Strip image data when saving to DB
		// Then recreate when retrieving
		protected string ParseImageData(string imageData)
		{
			//if (String.IsNullOrWhiteSpace(imageData))
			//	return "";

			// TODO: For now just return blank string from invalid images
			if (imageData.Contains(","))
			{
				imageData = imageData.Split(',')[1].Trim();
			}

			try
			{
				byte[] imageBytes = Convert.FromBase64String(imageData);

				using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

				Image image = Image.FromStream(ms, true);
				ImageFormat format = image.RawFormat;
				ImageCodecInfo codec = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == format.Guid);

				string mimeType = codec.MimeType;

				if (format.Equals(ImageFormat.Jpeg)
					|| format.Equals(ImageFormat.Png)
					|| format.Equals(ImageFormat.Gif))
				{
					return $"data:{mimeType};base64,{imageData}";
				}
			}
			catch
			{
				
			}

			return "";
		}
	}
}
