using System;

namespace InLife.Store.Core.Models
{
	public sealed class UserRole : Enumeration<string>
	{
		public static UserRole Admin          = new UserRole("Admin",          "Administrator");
		public static UserRole ContentManager = new UserRole("ContentManager", "Content Manager");
		public static UserRole Agent          = new UserRole("Agent",          "Agent");

		public UserRole() { }

		private UserRole(string id, string name) : base(id, name) { }

		public static UserRole FromId(string id)
		{
			return Enumeration<string>.FromId<UserRole>(id);
		}

		public static UserRole FromName(string name)
		{
			return Enumeration<string>.FromName<UserRole>(name);
		}
	}
}
