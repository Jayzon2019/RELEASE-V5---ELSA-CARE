using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System.IdentityModel.Tokens.Jwt;

using IdentityModel;
using IdentityModel.AspNetCore.OAuth2Introspection;
//using IdentityServer4;
//using IdentityServer4.AccessTokenValidation;

using InLife.Store.Core.Business;
using InLife.Store.Core.Settings;
using InLife.Store.Core.Services;
using InLife.Store.Core.Repository;

using InLife.Store.Infrastructure.Services;
using InLife.Store.Infrastructure.Repository;

using InLife.Store.Cms.Data;
using InLife.Store.Cms.Models;

namespace InLife.Store.Cms
{
	public class Startup
	{
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
			var connectionString = Configuration.GetConnectionString("DefaultConnection");

			services.AddDbContext<ApplicationContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddDbContext<IdentityContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IUserSessionRepository, UserSessionRepository>();

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

			services.AddTransient<IEmailService, EmailService>();

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

			//services
			//	.AddOptions<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme)
			//	.Configure<ITicketStore>((options, store) =>
			//	{
			//		options.SessionStore = store;
			//	});

			//TODO: Convert from mvc to razor pages
			//services.AddRazorPages();

			IdentityModelEventSource.ShowPII = true;
			JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services
				.AddAuthentication(options =>
				{
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
				})
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
				{
					options.SlidingExpiration = true;
					options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
					options.Cookie.MaxAge = TimeSpan.FromMinutes(5);
					options.Cookie.HttpOnly = true;
					options.Cookie.SameSite = SameSiteMode.Lax;
					options.SessionStore = new CustomTicketStore(services);
				})
				.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
				{
					options.Authority = Configuration.GetSection("Authentication:Authority").Value;
					options.ClientId = Configuration.GetSection("Authentication:ClientId").Value;
					options.ClientSecret = Configuration.GetSection("Authentication:ClientSecret").Value;
					options.ResponseType = IdentityModel.OidcConstants.ResponseTypes.Code;

					options.RequireHttpsMetadata = false;
					options.UsePkce = true;
					options.SaveTokens = true;
					//options.GetClaimsFromUserInfoEndpoint = true;
					options.UseTokenLifetime = true;

					options.ClaimActions.DeleteClaim("sid");
					options.ClaimActions.DeleteClaim("idp");
					//options.ClaimActions.MapAllExcept("aud", "iss", "iat", "nbf", "exp", "aio", "c_hash", "uti", "nonce");

					options.Scope.Add(IdentityModel.OidcConstants.StandardScopes.OpenId);
					options.Scope.Add(IdentityModel.OidcConstants.StandardScopes.Profile);

					//options.Scope.Add("IdentityServerApi");
					//options.Scope.Add("inlife.store.api");

					options.ClaimActions.MapJsonKey("role", "role");
					options.TokenValidationParameters = new TokenValidationParameters
					{
						RoleClaimType = "role"
					};
				});

			services
				.AddIdentityCore<ApplicationUser>
				(
					config =>
					{
						config.User.RequireUniqueEmail = true;
						config.Password.RequiredUniqueChars = 0;
						config.Password.RequireDigit = false;
						config.Password.RequireUppercase = false;
						config.Password.RequireNonAlphanumeric = false;
						config.Password.RequiredLength = 8;
						config.Lockout.MaxFailedAccessAttempts = 3;
						config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365000);
					}
				)
				.AddRoles<ApplicationRole>()
				.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>()
				.AddEntityFrameworkStores<IdentityContext>()
				.AddDefaultTokenProviders();

			//services.AddIdentity<ApplicationUser, ApplicationRole>
			//	(
			//		config =>
			//		{
			//			config.SignIn.RequireConfirmedEmail = false; //TODO: Implement email verification
			//			config.User.RequireUniqueEmail = true;
			//			config.Password.RequiredUniqueChars = 0;
			//			config.Password.RequireDigit = false;
			//			config.Password.RequireUppercase = false;
			//			config.Password.RequireNonAlphanumeric = false;
			//			config.Password.RequiredLength = 8;
			//			config.Lockout.MaxFailedAccessAttempts = 3;
			//		}
			//	)
			//	.AddEntityFrameworkStores<ApplicationContext>()
			//	.AddDefaultTokenProviders();

			services
				.AddRouting(options =>
				{
					options.LowercaseUrls = true;
				});

			services
				.AddControllersWithViews()
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
					options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
					options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				});

			// AutoMapper
			//services.AddAutoMapper(config => config.AddProfile<MappingProfile>(), typeof(Startup));

			services.AddCors();
			services.AddHttpContextAccessor();

			// App Settings
			services
				.Configure<UrlSettings>(Configuration.GetSection("Url"))
				.Configure<SmtpSettings>(Configuration.GetSection("Smtp"))
				.Configure<EmailSettings>(Configuration.GetSection("Email"));

			services.AddLogging(loggingBuilder =>
			{
				loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
				loggingBuilder.AddConsole();
				loggingBuilder.AddDebug();
			});
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

			app.UseAuthentication();
			app.UseAuthorization();

			//TODO: Convert from mvc to razor pages
			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapRazorPages();
			//});

			app.UseEndpoints(endpoints =>
			{
				endpoints
					.MapDefaultControllerRoute()
					.RequireAuthorization();
			});
		}
	}
}
