using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;


using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

using IdentityServer4.Services;
using IdentityServer4.AccessTokenValidation;
using IdentityModel.AspNetCore.OAuth2Introspection;

using InLife.Store.Identity.Data;
using InLife.Store.Identity.Models;
using InLife.Store.Identity.Services;
using InLife.Store.Identity.Infrastructure.FeatureFolders;
using InLife.Store.Identity.TokenProviders;
using InLife.Store.Identity.GrantValidators;


namespace InLife.Store.Identity
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public IWebHostEnvironment Environment { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration.GetConnectionString("DefaultConnection");

			services.AddDbContext<ApplicationContext>(options =>
				options.UseSqlServer(connectionString));

			services.Configure<ForwardedHeadersOptions>(options =>
			{
				options.ForwardedHeaders =
					ForwardedHeaders.XForwardedFor |
					ForwardedHeaders.XForwardedProto |
					ForwardedHeaders.XForwardedHost;
				options.KnownNetworks.Clear();
				options.KnownProxies.Clear();
			});

			services.AddIdentity<ApplicationUser, ApplicationRole>
				(
					config =>
					{
						config.SignIn.RequireConfirmedEmail = false; //TODO: Implement email verification
						config.User.RequireUniqueEmail = true;
						config.Password.RequiredUniqueChars = 0;
						config.Password.RequireDigit = false;
						config.Password.RequireUppercase = false;
						config.Password.RequireNonAlphanumeric = false;
						config.Password.RequiredLength = 1;
						config.Lockout.MaxFailedAccessAttempts = 3;
						config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365000); // 1000 years should be enough
					}
				)
				.AddEntityFrameworkStores<ApplicationContext>()
				.AddDefaultTokenProviders()
				.AddPasswordlessEmailTokenProvider()
				.AddPasswordlessPhoneTokenProvider();

			services.ConfigureApplicationCookie(options =>
			{
				options.SlidingExpiration = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
			});

			services
				.AddDataProtection()
				.SetApplicationName("InLife.Store.Identity")
				.SetDefaultKeyLifetime(TimeSpan.FromDays(7))
				.PersistKeysToFileSystem(new DirectoryInfo($@"{Environment.ContentRootPath}/Keys"));

			services
				.AddRouting(options =>
				{
					options.LowercaseUrls = true;
				});

			services
				.AddControllersWithViews()
				.AddFeatureFolders()
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
					options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
					options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				});

			services
				.AddRazorPages()
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
					options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
					options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				});

			services.AddTransient<IEmailSender, EmailSender>();
			services.AddTransient<IProfileService, ProfileService>();

			var idBuilder = services
				.AddIdentityServer(options =>
				{
					options.Events.RaiseErrorEvents = true;
					options.Events.RaiseInformationEvents = true;
					options.Events.RaiseFailureEvents = true;
					options.Events.RaiseSuccessEvents = true;

					foreach (var item in Config.GetCustomEndpoints())
						options.Discovery.CustomEntries.Add(item.Key, item.Value);
				})
				.AddExtensionGrantValidator<EmailCodeGrantValidator>()
				.AddExtensionGrantValidator<PhoneCodeGrantValidator>()
				.AddAspNetIdentity<ApplicationUser>()
				.AddProfileService<ProfileService>()
				.AddDeveloperSigningCredential()
				.AddInMemoryIdentityResources(Config.GetIdentityResources())
				.AddInMemoryApiResources(Config.GetApiResources())
				.AddInMemoryApiResources(Configuration.GetSection("IdentityServer:ApiResources"))
				//.AddInMemoryClients(Config.GetClients())
				.AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
				.AddOperationalStore(options =>
				{
					//options.DefaultSchema = "token";
					options.ConfigureDbContext = dbBuilder => dbBuilder.UseSqlServer(connectionString);
					options.EnableTokenCleanup = true;
					options.TokenCleanupInterval = 30;
				});

			services.AddCors();
			services.AddHttpContextAccessor();

			// Appsettings.json
			services.AddOptions()
				.Configure<AppSettings>(options =>
				{
					options.NotificationApi = Configuration.GetSection("URL:NotificationApi").Value;
				});

			services.AddLogging(loggingBuilder =>
			{
				loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
				loggingBuilder.AddConsole();
				loggingBuilder.AddDebug();
			});

			services.AddLocalApiAuthentication();
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

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseIdentityServer();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseCors(policy =>
			{
				policy
					.AllowAnyOrigin()
					.AllowAnyHeader()
					.AllowAnyMethod()
					.WithExposedHeaders("WWW-Authenticate");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
				endpoints.MapRazorPages();

			});
		}
	}
}
