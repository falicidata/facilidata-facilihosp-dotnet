using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Context;
using Facilidata.FaciliHosp.Infra.Identity.Interfaces;
using Facilidata.FaciliHosp.Infra.Identity.Repositories;
using Facilidata.FaciliHosp.Infra.Identity.Services;
using Facilidata.FaciliHosp.Infra.Identity.UnitOfWork;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Facilidata.FaciloHosp.Infra.Data.Repositories;
using Facilidata.FaciloHosp.Infra.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Facilidata.FaciliHosp.Infra.IoC
{
    public class NativeInject
    {
        public static void InjectDependecies(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // Injeção de Depedencia
            // Repositories
            services.AddScoped<ContextSQLS>();
            services.AddScoped<IUnitOfWork<ContextSQLS>, UnitOfWorkSQLS>();

            services.AddScoped<IHospitalRepository, HospitalRepository>();
            services.AddScoped<IExameRepository, ExameRepository>();

            // Identity
            services.AddScoped<IUsuarioAspNet, UsuarioAspNet>();
            services.AddScoped<IUnitOfWork<ContextIdentity>, UnitOfWorkIdentity>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();
            services.AddScoped<IMedicoRepository, MedicoRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            // 
        }
    }
}
