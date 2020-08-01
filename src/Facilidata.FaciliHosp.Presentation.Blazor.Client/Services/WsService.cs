using Facilidata.FaciliHosp.Infra.Identity.ViewModels;
using Facilidata.FaciliHosp.Presentation.Blazor.Client.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Facilidata.FaciliHosp.Presentation.Blazor.Client.Services
{
    public class WsService
    {
        private readonly HttpClient _httpClient;
        private readonly IServiceCollection _serviceCollection;
        private readonly ILogger<WsService> _logger;
        private readonly IMemoryCache _memoryCache;
        public WsService(HttpClient httpClient, ILogger<WsService> logger, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _logger = logger;
            _memoryCache = memoryCache;
            _serviceCollection = new ServiceCollection();
        }

        public async Task<TokenResponse> GetToken(string email, string senha)
        {
            try
            {
                var res = await _httpClient.PostAsJsonAsync("token", new LoginUsuarioViewModel() { Email = email, Senha = senha });
                var content = await res.Content.ReadFromJsonAsync<WsResult<TokenResponse>>();
                if (content.Success)
                {
                    _memoryCache.Set("TokenContainer", content?.Data);
                    SubstituiHttpClientServiceComAuthorize(content.Data.Token);
                    return content.Data;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }


        private void SubstituiHttpClientServiceComAuthorize(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            _serviceCollection.Replace(ServiceDescriptor.Transient((sp) => _httpClient));
        }

        
    }
}
