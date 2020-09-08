using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using M13.Domain.Constants;
using M13.Domain.Entities;
using M13.Domain.Interfaces;
using M13.Domain.Providers;
using M13.Domain.Services;
using M13.Infrastructure.Providers;

namespace M13.Infrastructure.RepositoryServices
{
    public class SpellService : ISpellService
    {
        #region Private fields

        private readonly HttpClientProvider _httpClientProvider;
        private readonly IRepository<Rule> _repository;
        private readonly ISpellServiceProvider _spellServiceProvider;

        private const int DefaultTextErrorsArrSize = 100;

        #endregion

        #region Ctor

        public SpellService(HttpClientProvider httpClientProvider, IRepository<Rule> repository, ISpellServiceProvider spellServiceProvider)
        {
            _httpClientProvider = httpClientProvider;
            _repository = repository;
            _spellServiceProvider = spellServiceProvider;
        }

        #endregion
        
        public async Task<ICollection<string>> GetErrorsByPageAsync(string page, CancellationToken cancellationToken)
        {
            var textErrors = new List<string>(DefaultTextErrorsArrSize);

            try
            {
                var site = new Uri($"{ProtocolConstants.DefaultHttpProtocol}{page}").Host;
                var htmlDocument = await _httpClientProvider.GetHtmlDocumentByPageAsync(page, cancellationToken);
                var xPath = _repository.GetAll()
                    .Where(x => x.Site == site)
                    .Select(x => x.Principle)
                    .FirstOrDefault();
            
                if (xPath == null)
                {
                    return textErrors;
                }
            
                var text = htmlDocument.DocumentNode
                    .SelectNodes(xPath)
                    .Aggregate(string.Empty, (current, node) => $"{current}{Environment.NewLine}{node.InnerText}");
            
                var errors = await _spellServiceProvider.GetErrorsByTextAsync(text, DefaultTextErrorsArrSize, cancellationToken);
                textErrors.AddRange(errors.Select(x => x.Word));
            }
            catch (Exception) {}
            
            return textErrors;
        }
    }
}