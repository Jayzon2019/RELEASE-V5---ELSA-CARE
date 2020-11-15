using System;
namespace InLife.Store.Api.Messages
{
	public class BaseResponse
	{
		public BaseResponse()
		{
		}

		// This is a hack, old uploaded images doesn't have an image data
		// Clean this up when StoreFront has been updated
		protected string ParseImageData(string imageData)
		{
			return imageData.Contains(",")
				? imageData.Split(",")[1].Trim()
				: imageData;
		}
	}
}
