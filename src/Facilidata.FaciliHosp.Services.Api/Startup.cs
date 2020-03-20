using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Facilidata.FaciloHosp.Infra.Data.Repositories;
using Facilidata.FaciloHosp.Infra.Data.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Facilidata.FaciliHosp.Services.Api
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
            services.AddControllers();
            // Infra Data
            string connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<ContextSQLS>(options => options.UseSqlServer(connectionString));

            // Repositories
            services.AddScoped<ContextSQLS>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IExameRepository, ExameRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
