using System.Threading.Tasks;
using System.Collections.Generic;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Core.Services
{
	public interface ICurrentUserProvider
	{
		User Get();
	}
}
