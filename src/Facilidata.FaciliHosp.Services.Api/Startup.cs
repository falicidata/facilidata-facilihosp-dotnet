using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Facilidata.FaciliHosp.Application.AutoMapperProfiles;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Models;
using Facilidata.FaciliHosp.Infra.Identity.Repositories;
using Facilidata.FaciliHosp.Infra.Identity.Services;
using Facilidata.FaciliHosp.Infra.Identity.UnitOfWork;
using Facilidata.FaciliHosp.Infra.IoC;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Facilidata.FaciloHosp.Infra.Data.Repositories;
using Facilidata.FaciloHosp.Infra.Data.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Facilidata.FaciliHosp.Services.Api.Configurations.JWT;
using Microsoft.Extensions.Options;
using Facilidata.FaciliHosp.Services.Api.Configurations;
using Microsoft.AspNetCore.Mvc.Authorization;

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
            services.AddAuthenticationConfiguration(Configuration);

            services.AddMvcConfiguration();

            services.AddControllers();

            services.AddCors(opt => { opt.AddPolicy("All", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build()); });
         
            services.AddIdentity<Usuario, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ContextIdentity>();

            // Injeção de Depedencia
            NativeInject.InjectDependecies(services);

            services.AddSwaggerConfiguration();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("All");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerConfiguration();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
