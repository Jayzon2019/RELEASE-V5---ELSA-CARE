using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using AutoMapper;

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
			// DB Context
			var defaultConnectionString = Configuration.GetConnectionString("DefaultConnection");

			services
				.AddDbContext<ApplicationContext>(options =>
					options.UseSqlServer(defaultConnectionString));

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
				//options.AddPolicy(AllowSpecificOrigins,
				//	builder =>
				//	{
				//		builder
				//			.WithOrigins(allowedOrigins)
				//			.AllowAnyHeader()
				//			.AllowAnyMethod()
				//			.WithExposedHeaders("WWW-Authenticate");
				//	});

				options.AddPolicy(AllowSpecificOrigins,
					builder =>
					{
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

			services.AddTransient<ICustomerRepository, CustomerRepository>();
			services.AddTransient<IQuoteRepository, QuoteRepository>();

			services.AddTransient<IActivityLogRepository, ActivityLogRepository>();
			services.AddTransient<IKeyMetricRepository, KeyMetricRepository>();
			services.AddTransient<IFaqCategoryRepository, FaqCategoryRepository>();
			services.AddTransient<IFaqRepository, FaqRepository>();
			services.AddTransient<IFooterLinkRepository, FooterLinkRepository>();
			services.AddTransient<IHeroRepository, HeroRepository>();
			services.AddTransient<IPrimeCareRepository, PrimeCareRepository>();
			services.AddTransient<IPrimeHeroRepository, PrimeHeroRepository>();
			services.AddTransient<IProductDetailRepository, ProductDetailRepository>();
			services.AddTransient<IProductRepository, ProductRepository>();

			services.AddTransient<IContentManagement, ContentManagement>();
			services.AddTransient<IOrderProcessing, OrderProcessing>();

			services.AddTransient<IEmailService, EmailService>();
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
