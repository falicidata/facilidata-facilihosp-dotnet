using Facilidata.FaciliHosp.Presentation.Blazor.Client.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Presentation.Blazor.Client.HttpClientHandlers
{
    public class HttpSendMessageHandler : DelegatingHandler
    {
        private readonly IServiceCollection _services;

        public HttpSendMessageHandler(IServiceCollection services)
        {
            InnerHandler = new HttpClientHandler();
            this._services = services;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var memoryCache = _services.BuildServiceProvider().GetService<IMemoryCache>();

            bool existTokenContainer = memoryCache.TryGetValue("TokenContainer", out TokenResponse tokenResponse);
            Console.WriteLine(existTokenContainer);

            if (existTokenContainer && !string.IsNullOrEmpty(tokenResponse.Token))
            {
                Console.WriteLine("Send on Authorization");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", tokenResponse.Token);
                return await base.SendAsync(request, cancellationToken);
            }

            Console.WriteLine("Send no Authorization");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
