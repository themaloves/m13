using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using M13.Domain.Models.Spells;
using M13.Domain.Providers;
using Newtonsoft.Json;

namespace M13.Infrastructure.Providers
{
    public class YandexSpellServiceProvider : ISpellServiceProvider
    {
        #region Private fields

        private readonly HttpClient _httpClient;

        private const string CheckTextAction = "checkText";

        #endregion

        #region Ctor

        public YandexSpellServiceProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        #endregion
        
        public async Task<List<SpellErrorModel>> GetErrorsByTextAsync(string text, int maxCount, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder($"{_httpClient.BaseAddress}{CheckTextAction}");

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["text"] = text;
            uriBuilder.Query = query.ToString();
            
            var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
            var response = await _httpClient.SendAsync(request, cancellationToken);

            var content = await response.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<SpellErrorModel[]>(content);
            
            var result = new List<SpellErrorModel>(maxCount);
            result.AddRange(errors);

            return result;
        }
    }
}