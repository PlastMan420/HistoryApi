using HistoryMockApi.Data;
using HistoryMockApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoryMockApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
			services.AddHttpContextAccessor();
			services.AddSingleton<IUriService>(o =>
			{
				var accessor = o.GetRequiredService<IHttpContextAccessor>();
				var request = accessor.HttpContext.Request;
				var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
				return new UriService(uri);
			});
			services.AddControllers();
			
			services.AddCors();

			//services.AddTransient<Seed>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			//seeder.SeedUsers();
			app.UseHttpsRedirection();

			app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			
			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
