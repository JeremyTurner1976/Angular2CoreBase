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
	using Common.Interfaces;
	using Common.Services.WeatherServices;

	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
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
			// Add framework services.
			services.AddMvc();
			services.AddLogging();

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

			//Open Weather Api
			services.AddSingleton<IWeatherServiceSettings, OpenWeatherServiceSettings> ();
			services.AddTransient<IWeatherService, OpenWeatherService>();

			services.AddSingleton<IRepository<ApplicationUser>, CoreBaseRepository<ApplicationUser>>();
			services.AddSingleton<IRepository<Error>, CoreBaseRepository<Error>>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			if (Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();

				using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
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
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

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