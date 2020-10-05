using System;
using Microsoft.Extensions.DependencyInjection;

namespace InLife.Store.Identity.Infrastructure.FeatureFolders
{
	public static class ServiceCollectionExtensions
	{
		public static IMvcBuilder AddFeatureFolders(this IMvcBuilder services)
		{
			if (services == null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			services.AddRazorOptions(options =>
			{
				options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
			});

			return services;
		}
	}
}
