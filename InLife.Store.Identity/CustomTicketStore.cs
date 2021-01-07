using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

using InLife.Store.Core.Models;
using InLife.Store.Core.Repository;

namespace InLife.Store.Identity
{
	public class CustomTicketStore : ITicketStore
	{
		private readonly IServiceCollection services;

		public CustomTicketStore
		(
			IServiceCollection services
		)
		{
			this.services = services;
		}

		public async Task RemoveAsync(string key)
		{
			await Task.Delay(0);

			if (Guid.TryParse(key, out var id))
			{
				using var scope = services.BuildServiceProvider().CreateScope();
				var userSessionRepository = scope.ServiceProvider.GetService<IUserSessionRepository>();

				var model = userSessionRepository.Get(id);
				if (model != null)
				{
					userSessionRepository.Delete(model);
				}
			}
		}

		public async Task RenewAsync(string key, AuthenticationTicket ticket)
		{
			await Task.Delay(0);

			if (Guid.TryParse(key, out var id))
			{
				using var scope = services.BuildServiceProvider().CreateScope();
				var userSessionRepository = scope.ServiceProvider.GetService<IUserSessionRepository>();

				var model = userSessionRepository.Get(id);
				if (model != null)
				{
					model.Value = SerializeToBytes(ticket);
					model.LastActivity = DateTimeOffset.UtcNow;
					model.Expires = ticket.Properties.ExpiresUtc;

					userSessionRepository.Update(model);
				}
			}
		}

		public async Task<AuthenticationTicket> RetrieveAsync(string key)
		{
			await Task.Delay(0);

			if (Guid.TryParse(key, out var id))
			{
				using var scope = services.BuildServiceProvider().CreateScope();
				var userSessionRepository = scope.ServiceProvider.GetService<IUserSessionRepository>();

				var model = userSessionRepository.Get(id);
				if (model != null)
				{
					model.LastActivity = DateTimeOffset.UtcNow;
					userSessionRepository.Update(model);

					return DeserializeFromBytes(model.Value);
				}
			}

			return null;
		}

		public async Task<string> StoreAsync(AuthenticationTicket ticket)
		{
			await Task.Delay(0);

			var userId = ticket.Principal.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;

			using var scope = services.BuildServiceProvider().CreateScope();
			var userSessionRepository = scope.ServiceProvider.GetService<IUserSessionRepository>();
			var userRepository = scope.ServiceProvider.GetService<IUserRepository>();

			var user = userRepository.Get(userId);
			if (user == null)
				return null;

			var model = new UserSession
			{
				User = user,
				LastActivity = DateTimeOffset.UtcNow,
				Value = SerializeToBytes(ticket)
			};

			var expiresUtc = ticket.Properties.ExpiresUtc;
			if (expiresUtc.HasValue)
			{
				model.Expires = expiresUtc.Value;
			}

			userSessionRepository.Create(model);

			return model.Id.ToString();
		}

		private byte[] SerializeToBytes(AuthenticationTicket source)
			=> TicketSerializer.Default.Serialize(source);

		private AuthenticationTicket DeserializeFromBytes(byte[] source)
			=> source == null ? null : TicketSerializer.Default.Deserialize(source);
	}
}
