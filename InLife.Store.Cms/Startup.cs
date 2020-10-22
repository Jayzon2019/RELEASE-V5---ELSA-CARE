using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
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

			//TODO: Convert from mvc to razor pages
			//services.AddRazorPages();

			JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

			services
				.AddAuthentication(options =>
				{
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
				})
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
				{
					options.Authority = Configuration.GetSection("Authentication:Authority").Value;
					options.ClientId = Configuration.GetSection("Authentication:ClientId").Value;
					options.ClientSecret = Configuration.GetSection("Authentication:ClientSecret").Value;
					options.ResponseType = IdentityModel.OidcConstants.ResponseTypes.Code;
					//options.ResponseMode = IdentityModel.OidcConstants.ResponseModes.Query;
					//options.RequireHttpsMetadata = false;
					options.UsePkce = true;
					options.SaveTokens = true;
					//options.GetClaimsFromUserInfoEndpoint = true;

					options.Scope.Add(IdentityModel.OidcConstants.StandardScopes.OpenId);
					options.Scope.Add(IdentityModel.OidcConstants.StandardScopes.Profile);
					//options.Scope.Add("role");
					//options.Scope.Add("IdentityServerApi");
					//options.Scope.Add("inlife.store.api");
				});

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

			services
				.Configure<ForwardedHeadersOptions>(options =>
				{
					options.ForwardedHeaders =
						ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
				});

			// AutoMapper
			//services.AddAutoMapper(config => config.AddProfile<MappingProfile>(), typeof(Startup));

			services.AddCors();
			services.AddHttpContextAccessor();

			// Appsettings.json
			//services.AddOptions()
			//	.Configure<AppSettings>(options =>
			//	{
			//		options.NotificationApi = Configuration.GetSection("URL:NotificationApi").Value;
			//	});

			services.AddLogging(loggingBuilder =>
			{
				loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
				loggingBuilder.AddConsole();
				loggingBuilder.AddDebug();
			});

			services.AddTransient<IUserRepository, UserRepository>();

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
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseForwardedHeaders();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseForwardedHeaders();
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
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
