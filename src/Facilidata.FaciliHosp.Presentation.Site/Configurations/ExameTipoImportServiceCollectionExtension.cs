using Facilidata.FaciliHosp.Domain.Interfaces;
using Facilidata.FaciloHosp.Infra.Data.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Presentation.Site.Configurations
{
    public static class ExameTipoImportServiceCollectionExtension
    {
        public static void AddExameTipoImport(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                                    .AddJsonFile("exameTipos.json")
                                    .Build();

            var exameTipoImport = new ExameTipoImport();
            new ConfigureFromConfigurationOptions<ExameTipoImport>(configuration.GetSection("ExameTipoImport")).Configure(exameTipoImport);
            services.AddSingleton(exameTipoImport);

            var uow = services.BuildServiceProvider().GetService<IUnitOfWork<ContextSQL>>();
            var exameTipoRepository = services.BuildServiceProvider().GetService<IExameTipoRepository>();
          
            try
            {
                Task.Run(() =>
                {
                    foreach (var tipo in exameTipoImport.Tipos)
                        exameTipoRepository.InsereSeNaoExistir(tipo);
                });

            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }
    }
}
