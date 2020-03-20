using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaciliHosp.Domain.Interfaces;
using FaciliHosp.Infra.Data.Context;
using FaciliHosp.Infra.Data.Repositorios;
using FaciliHosp.Infra.Data.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Facilidata.Services.Api
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

            string conn = Configuration.GetConnectionString("Default");
            services.AddDbContext<ContextSQLServer>(opt => opt.UseSqlServer(conn));


            // Infra
            services.AddScoped<ContextSQLServer>();
            services.AddScoped<IHospitalRepositorio, HospitalRepositorio>();
            services.AddScoped<IAtendimentoRepositorio, AtendimentoRepositorio>();
            services.AddScoped<IExameRepositorio, ExameRepositorio>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

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
