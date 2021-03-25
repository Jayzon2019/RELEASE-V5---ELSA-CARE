using System;
using System.IO;
using System.Reflection;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

using InLife.Store.Core.Models;
using InLife.Store.Core.Business;
using InLife.Store.Core.Settings;
using InLife.Store.Core.Services;
using InLife.Store.Core.Repository;

using InLife.Store.Infrastructure.Services;
using InLife.Store.Infrastructure.Repository;


[assembly: FunctionsStartup(typeof(InLife.Store.Functions.Startup))]
namespace InLife.Store.Functions
{
	class Startup : FunctionsStartup
	{
		public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
		{
			//		var executioncontextoptions = builder.Services.BuildServiceProvider()
			//.GetService<IOptions<ExecutionContextOptions>>().Value;
			//		var currentDirectory = executioncontextoptions.AppDirectory;

			var localBasePath = Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot");
			var azureBasePath = $"{Environment.GetEnvironmentVariable("HOME")}/site/wwwroot";
			var basePath = localBasePath ?? azureBasePath;

			FunctionsHostBuilderContext context = builder.GetContext();

			builder.ConfigurationBuilder
				.SetBasePath(basePath)
				.AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
				.AddJsonFile($"appsettings.{context.EnvironmentName}.json", optional: true, reloadOnChange: false)
				.AddUserSecrets(Assembly.GetExecutingAssembly(), true, true)
				.AddEnvironmentVariables();
		}

		public override void Configure(IFunctionsHostBuilder builder)
		{
			var configuration = builder.GetContext().Configuration;

			// Connection Strings
			builder.Services
				.AddDbContext<ApplicationContext>(options =>
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
			builder.Services
				.AddDbContext<PrimeCareContext>(options =>
					options.UseSqlServer(configuration.GetConnectionString("PrimeCareConnection")));
			builder.Services
				.AddDbContext<GroupContext>(options =>
					options.UseSqlServer(configuration.GetConnectionString("GroupConnection")));

			// App Settings
			builder.Services
				.Configure<UrlSettings>(configuration.GetSection("Url"))
				.Configure<SmtpSettings>(configuration.GetSection("Smtp"))
				.Configure<EmailSettings>(configuration.GetSection("Email"))
				.Configure<ExternalServices>(configuration.GetSection("ExternalServices"))
				.AddOptions();

			//builder.Services.AddOptions<UrlSettings>()
			//	.Configure<IConfiguration>((settings, configuration) =>
			//	{
			//		configuration.GetSection("Url").Bind(settings);
			//	});
			//builder.Services.AddOptions<SmtpSettings>()
			//	.Configure<IConfiguration>((settings, configuration) =>
			//	{
			//		configuration.GetSection("Smtp").Bind(settings);
			//	});
			//builder.Services.AddOptions<EmailSettings>()
			//	.Configure<IConfiguration>((settings, configuration) =>
			//	{
			//		configuration.GetSection("Email").Bind(settings);
			//	});
			//builder.Services.AddOptions<ExternalServices>()
			//	.Configure<IConfiguration>((settings, configuration) =>
			//	{
			//		configuration.GetSection("ExternalServices").Bind(settings);
			//	});

			// General
			builder.Services.AddTransient<IEmailService, EmailService>();
			builder.Services.AddTransient<ISftpService, SftpService>();

			// Product - PrimeCare
			builder.Services.AddScoped<IPrimeCareApplicationRepository, PrimeCareApplicationRepository>();

			// Product - Group
			builder.Services.AddScoped<IGroupApplicationProcessing, GroupApplicationProcessing>();
			builder.Services.AddScoped<IGroupApplicationRepository, GroupApplicationRepository>();
			builder.Services.AddScoped<IGroupFileRepository, GroupFileRepository>();
		}
	}
}
