using Facilidata.FaciliHosp.Infra.Identity.Claims;
using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Facilidata.FaciliHosp.Infra.IoC;
using Facilidata.FaciliHosp.Presentation.Site.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.IO;

namespace Facilidata.FaciliHosp.Presentation.Site
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
            services.AddControllersWithViews();

            // Infra Data
            string connectionString = Configuration.GetConnectionString("Default");

 
        

            // Identity

            services.AddIdentity<Usuario, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ContextIdentity>()
                .AddClaimsPrincipalFactory<UsuarioClaimsPrincipalFactory>();

            services.AddMvcConfiguration();
            services.AddAuthenticationConfiguration(Configuration);


            // Injeção de Depedencia
            NativeInject.InjectDependecies(services);

            services.AddExameTipoImport();

            // Cria pasta temporaria se não existir
            string currentPath = Directory.GetCurrentDirectory();
            string pathTmp = Path.Combine(currentPath, "tmp");
            bool exists =  Directory.Exists(pathTmp);
            if(!exists)
                Directory.CreateDirectory(Path.Combine(currentPath,"tmp"));

            services.AddSwaggerConfiguration();

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
            }

            app.UseSwaggerConfiguration();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
