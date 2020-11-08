using AutoMapper;
using Facilidata.FaciliHosp.Application.AutoMapperProfiles;
using Facilidata.FaciliHosp.Application.Interfaces;
using Facilidata.FaciliHosp.Application.Services;
using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Repositories;
using Facilidata.FaciliHosp.Infra.Identity.Services;
using Facilidata.FaciliHosp.Infra.Identity.UnitOfWork;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Facilidata.FaciloHosp.Infra.Data.Repositories;
using Facilidata.FaciloHosp.Infra.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Facilidata.FaciliHosp.Infra.IoC
{
    public class NativeInject
    {
        public static void InjectDependecies(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Injeção de Depedencia
            // Repositories
            services.AddDbContext<ContextSQL>();
            services.AddScoped<IUnitOfWork<ContextSQL>, UnitOfWorkSQLS>();

            services.AddScoped<IExameTipoRepository, ExameTipoRepository>();
            services.AddScoped<IExameRepository, ExameRepository>();
            services.AddScoped<IContaRepository, ContaRepository>();
            services.AddScoped<IPlanoRepository, PlanoRepository>();
            services.AddScoped<IExameCompRepository, ExameCompRepository>();

            // Identity
            services.AddDbContext<ContextIdentity>();
            services.AddScoped<IUsuarioAspNet, UsuarioAspNet>();
            services.AddScoped<IUnitOfWork<ContextIdentity>, UnitOfWorkIdentity>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IPlanoService, PlanoService>();

            // Services
            services.AddScoped<IExameService, ExameService>();
            services.AddScoped<IAzureStorageService, AzureStorageService>();

            // Application
            services.AddAutoMapper(typeof(ViewModelToModel));
        }
    }
}
