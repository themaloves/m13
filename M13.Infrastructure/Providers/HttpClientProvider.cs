using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using M13.Domain.Constants;

namespace M13.Infrastructure.Providers
{
    /// <summary>
    /// Базовый HttpClientProvider
    /// </summary>
    public class HttpClientProvider
    {
        #region Private fields

        private readonly HttpClient _httpClient;

        #endregion

        #region Ctor

        public HttpClientProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion
        
        public async Task<HtmlDocument> GetHtmlDocumentByPageAsync(string page, CancellationToken cancellationToken)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"{ProtocolConstants.DefaultHttpProtocol}{page}", cancellationToken);
            
            var document = new HtmlDocument();
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            document.LoadHtml(content);

            return document;
        }
    }
}