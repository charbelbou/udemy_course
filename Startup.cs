using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using udemy_course.Persistence;
using udemy.Persistence;
using udemy_course1.Core.Models;
using udemy_course1.Core;
using udemy_course1.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using udemy_course1.Controllers;

namespace udemy_course1
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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://dev-r8lrb84i.us.auth0.com/";
                options.Audience = "https://api.course.com";
            });
            //Configure the photo settings
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));
            // Mapping interfaces to implementations
            services.AddScoped<IPhotoRepository,PhotoRepository>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddTransient<IPhotoService,PhotoService>();
            services.AddTransient<IPhotoStorage,FileSystemPhotoStorage>();
            services.AddScoped<IVehicleRepository,VehicleRepository>();

            // Adding auto mapper through dependency injection
            services.AddAutoMapper(typeof(Startup));
            // Add the DbContext we've made as a service through dependency injection (DI)
            services.AddDbContext<UdemyDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));
            // Add authorization, requiring "Admin" in "https://vega.com/roles" in the JWT.
            services.AddAuthorization(options =>{
                options.AddPolicy(Policies.RequireAdminRole,policy=>policy.RequireClaim("https://vega.com/roles","Admin"));
            });
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
