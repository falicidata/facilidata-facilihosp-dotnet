using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Facilidata.FaciliHosp.Services.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "FaciliHosp",
                    Version = "v1"
                });

            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {

            app.UseSwaggerUI(options =>
            {

                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Facilihosp");
                options.RoutePrefix = "docs";

            });

        }
    }
}
