using Angular2CoreBase.Data;
using Angular2CoreBase.Data.Database;
using Angular2CoreBase.Data.Interfaces;
using Angular2CoreBase.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Angular2CoreBase.Ui
{
	using System;
	using Common.CommonModels.ConfigSettings;
	using Common.CommonModels.WeatherService.DarkSkyWeather;
	using Common.Extensions;
	using Common.Interfaces;
	using Common.Interfaces.WeatherService;
	using Common.Middleware;
	using Common.Services;
	using Common.Services.WeatherServices;
	using Data.Decorators;
	using Data.Services.LoggingServices;
	using Newtonsoft.Json.Serialization;
	using Data.Extensions;
	using Microsoft.Extensions.Options;

	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			IConfigurationBuilder builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
			Environment = env;
		}

		public IConfigurationRoot Configuration { get; }
		private IHostingEnvironment Environment { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//http://stackoverflow.com/questions/38138100/what-is-the-difference-between-services-addtransient-service-addscope-and-servi
			//Singleton which creates a single instance throughout the application.
			//It creates the instance for the first time and reuses the same object in the all calls.

			//Scoped lifetime services are created once per request within the scope.It is equivalent to Singleton 
			//in the current scope.eg. in MVC it creates 1 instance per each http request but uses the same instance 
			//in the other calls within the same web request.

			//Transient lifetime services are created each time they are requested.
			//This lifetime works best for lightweight, stateless services.

			//services.AddTransient<IOperationTransient, Operation>();
			//services.AddScoped<IOperationScoped, Operation>();
			//services.AddSingleton<IOperationSingleton, Operation>();
			//services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));
			//services.AddTransient<OperationService, OperationService>();

			services.AddLogging();

			//Configuration Pocos
			services.AddOptions();
			services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
			services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

			//Dark Sky Api
			services.AddSingleton<IWeatherServiceSettings, DarkSkyWeatherServiceSettings>();
			services.AddTransient<IWeatherService, DarkSkyWeatherService>();

			//Open Weather Api
			//services.AddSingleton<IWeatherServiceSettings, OpenWeatherServiceSettings>();
			//services.AddTransient<IWeatherService, OpenWeatherService>();

			//DataBase Setups
			//if (Environment.IsDevelopment())
			//{
			//	services.AddDbContext<CoreBaseContext>(options =>
			//		options.UseInMemoryDatabase(Configuration.GetConnectionString("CoreBaseConnectionString")));
			//}
			//else
			//{
			services.AddDbContext<CoreBaseContext>(options =>
					options.UseSqlServer(Configuration.GetConnectionString("CoreBaseConnectionString")));
			//}

			//Add data classes
			services.AddSingleton<IRepository<ApplicationUser>, CoreBaseRepository<ApplicationUser>>();
			services.AddSingleton<IRepository<Error>, CoreBaseRepository<Error>>();
			services.AddScoped<ITrackedModelDecorator<Error>, TrackedModelDecorator<Error>>();

			//Add custom services
			services.AddTransient<IEmailService, EmailService>();
			services.AddSingleton<IFileService, FileService>();
			services.AddSingleton<IDatabaseLoggingService, DatabaseLoggingService>();

			// Add framework services.
			IMvcBuilder mvcBuilder = services.AddMvc();
			mvcBuilder.AddJsonOptions
				(opts => opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

			services.AddMemoryCache(opt => opt.ExpirationScanFrequency = TimeSpan.FromMinutes(5));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(
			IApplicationBuilder app, 
			IHostingEnvironment env, 
			ILoggerFactory loggerFactory, 
			IEmailService mailService,
			IOptions<EmailSettings> emailSettings,
			IFileService fileService,
			IRepository<Error> errorRepository,
			ITrackedModelDecorator<Error> errorDecorator)
		{
			/*
			public enum LogLevel
			{
				Debug = 1,
				Verbose = 2,
				Information = 3,
				Warning = 4,
				Error = 5,
				Critical = 6,
				None = int.MaxValue
			}

			*/
			loggerFactory.AddEmailLogger(mailService, emailSettings, LogLevel.Information);
			loggerFactory.AddDatabaseLogger(errorRepository, errorDecorator, LogLevel.Error);

			if (Environment.IsDevelopment())
			{
				//Detailed trace logs (Including Requests and Responses)
				//loggerFactory.AddFileLogger(fileService, LogLevel.Information);

				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
				loggerFactory.AddDebug(LogLevel.Information);
				loggerFactory.AddConsole(Configuration.GetSection("Logging"));

				using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
				{
					serviceScope.ServiceProvider.GetService<CoreBaseContext>().Database.Migrate();
					serviceScope.ServiceProvider.GetService<CoreBaseContext>().SeedData();
				}

				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				{
					HotModuleReplacement = true
				});
			}
			else
			{
				//Detailed trace logs
				//loggerFactory.AddFileLogger(fileService, LogLevel.Information);

				loggerFactory.AddEmailLogger(mailService, emailSettings, LogLevel.Error);
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseMiddleware<HttpContextLogging>();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

				routes.MapSpaFallbackRoute(
					name: "spa-fallback",
					defaults: new {controller = "Home", action = "Index"});
			});
		}
	}
}