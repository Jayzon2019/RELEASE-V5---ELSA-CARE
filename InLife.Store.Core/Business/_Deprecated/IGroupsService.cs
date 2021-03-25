using InLife.Store.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InLife.Store.Core.Business
{
	public interface IGroupsService
	{
		GroupsT SaveGroups(GroupsT groupsT);
	}
}
