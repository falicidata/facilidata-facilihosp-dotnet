using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Facilidata.FaciliHosp.Presentation.Site.Configurations
{
    public static class MvcConfiguration
    {
        public static void AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                //var filtroCookie = new AuthorizationPolicyBuilder()
                //  .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                //  .RequireAuthenticatedUser().Build();

                //var filtroJwt = new AuthorizationPolicyBuilder()
                //    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                //    .RequireAuthenticatedUser().Build();

                //options.Filters.Add(new AuthorizeFilter(filtroCookie));
                //options.Filters.Add(new AuthorizeFilter(filtroJwt));
         

            }).AddNewtonsoftJson(
                options => {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }
            );
        }
    }
}
