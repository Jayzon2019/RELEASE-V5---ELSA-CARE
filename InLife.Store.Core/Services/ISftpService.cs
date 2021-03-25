using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using InLife.Store.Core.Models;

namespace InLife.Store.Core.Services
{
	public interface ISftpService
	{
		#region Business - Group

		Task UploadGroupFile(string directory, string filename, Stream stream);

		Task<string> UploadGroupFile(GroupApplication application, string documentType, string contentType, Stream stream);

		Task UploadGroupApplicationData(GroupApplication application);

		Task UploadGroupApplicationsBatchData(IEnumerable<GroupApplication> applications);

		#endregion
	}
}
