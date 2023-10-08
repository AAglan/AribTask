using AribTask.Data;
using AribTask.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

//using AribTask.Permission;

namespace AribTask
{
	public class Startup
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

		public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			Configuration = configuration;
			_webHostEnvironment = webHostEnvironment;
		}
		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddDbContext<ApplicationDbContext>(options =>
			 options.UseSqlServer(
		 Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<IdentityUser, IdentityRole>(
			options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireDigit = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 4;
			})
			.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
			services.AddSingleton<IWebHostEnvironment>(_webHostEnvironment);
			services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
			services.AddAutoMapper(typeof(Startup));

		}




		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();


			// Other middleware and configuration

			// Set the web root path
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(_webHostEnvironment.WebRootPath),
				RequestPath = "/images" // This should match the path you are using in Path.Combine
			});
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
									name: "admin",
									pattern: "{area:exists}/{controller=EmployeeTask}/{action=Index}/{id?}"
							);

				endpoints.MapControllerRoute(
									name: "default",
									pattern: "{controller=Account}/{action=Login}/{id?}"
							);
			});

		}
	}
}
