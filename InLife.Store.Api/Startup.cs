using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using AutoMapper;

using InLife.Store.Core.Models;
using InLife.Store.Core.Business;
using InLife.Store.Core.Settings;
using InLife.Store.Core.Services;
using InLife.Store.Core.Repository;

using InLife.Store.Infrastructure.Services;
using InLife.Store.Infrastructure.Repository;

namespace InLife.Store.Api
{
	public class Startup
	{
		private const string AllowSpecificOrigins = "AllowSpecificOrigins";

		public IWebHostEnvironment Environment { get; }
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// If using Kestrel:
			services.Configure<KestrelServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});

			// If using IIS:
			services.Configure<IISServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});

			// DB Context
			services
				.AddDbContext<ApplicationContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services
				.AddDbContext<PrimeCareContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("PrimeCareConnection")));
			services
				.AddDbContext<GroupContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("GroupConnection")));
			//services
			//	.AddDbContext<PrimeSecureContext>(options =>
			//		options.UseSqlServer(Configuration.GetConnectionString("PrimeSecureConnection")));

			// App Settings
			services
				.Configure<UrlSettings>(Configuration.GetSection("Url"))
				.Configure<SmtpSettings>(Configuration.GetSection("Smtp"))
				.Configure<EmailSettings>(Configuration.GetSection("Email"))
				.Configure<ExternalServices>(Configuration.GetSection("ExternalServices"));

			services
				.Configure<ForwardedHeadersOptions>(options =>
				{
					options.ForwardedHeaders =
						ForwardedHeaders.XForwardedFor |
						ForwardedHeaders.XForwardedProto |
						ForwardedHeaders.XForwardedHost;
					options.KnownNetworks.Clear();
					options.KnownProxies.Clear();
				});

			var allowedOrigins = Configuration.GetSection("AllowedOrigins").Get<string[]>();
			services.AddCors(options =>
			{
				options.AddPolicy(AllowSpecificOrigins,
					builder =>
					{
						//builder
						//	.WithOrigins(allowedOrigins)
						//	.AllowAnyHeader()
						//	.AllowAnyMethod()
						//	.WithExposedHeaders("WWW-Authenticate");

						builder
							.AllowAnyOrigin()
							.AllowAnyHeader()
							.AllowAnyMethod()
							.WithExposedHeaders("WWW-Authenticate");
					});
			});

			services
				.AddRouting(options =>
				{
					options.LowercaseUrls = true;
				});

			services
				.AddControllers()
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
					options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
					options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				});

			services.AddLogging(loggingBuilder =>
			{
				loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
				loggingBuilder.AddConsole();
				loggingBuilder.AddDebug();
			});

			// AutoMapper
			services.AddAutoMapper(config => config.AddProfile<MappingProfile>(), typeof(Startup));

			// General
			services.AddTransient<IEmailService, EmailService>();
			services.AddTransient<ISftpService, SftpService>();

			// Product - PrimeCare
			services.AddScoped<IPrimeCareApplicationRepository, PrimeCareApplicationRepository>();

			// Product - Group
			services.AddScoped<IGroupApplicationProcessing, GroupApplicationProcessing>();
			services.AddScoped<IGroupApplicationRepository, GroupApplicationRepository>();
			services.AddScoped<IGroupFileRepository, GroupFileRepository>();

			// Product - PrimeSecure
			services.AddScoped<IPrimeSecureApplicationProcessing, PrimeSecureApplicationProcessing>();
			services.AddScoped<IPrimeSecureApplicationRepository, PrimeSecureApplicationRepository>();

			// Content
			services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
			services.AddScoped<IKeyMetricRepository, KeyMetricRepository>();
			services.AddScoped<IFaqCategoryRepository, FaqCategoryRepository>();
			services.AddScoped<IFaqRepository, FaqRepository>();
			services.AddScoped<IFooterLinkRepository, FooterLinkRepository>();
			services.AddScoped<IHeroRepository, HeroRepository>();
			services.AddScoped<IPrimeCareRepository, PrimeCareRepository>();
			services.AddScoped<IPrimeHeroRepository, PrimeHeroRepository>();
			services.AddScoped<IProductDetailRepository, ProductDetailRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var customDomainProtocol = Configuration.GetSection("CustomDomain:Protocol").Value;
			var customDomainHost     = Configuration.GetSection("CustomDomain:Host").Value;
			var customDomainPathBase = Configuration.GetSection("CustomDomain:PathBase").Value;
			if (!String.IsNullOrWhiteSpace(customDomainProtocol) && !String.IsNullOrWhiteSpace(customDomainHost))
			{
				app.Use((context, next) =>
				{
					context.Request.Protocol = customDomainProtocol;
					context.Request.Host = new HostString(customDomainHost);
					context.Request.PathBase = new PathString(customDomainPathBase);
					return next();
				});
			}

			app.UseForwardedHeaders();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors(AllowSpecificOrigins);
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
