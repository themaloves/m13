using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using M13.Domain.Conventions;
using M13.Domain.Entities;
using M13.Domain.Interfaces;
using M13.Domain.Models.Rules;
using M13.Domain.Services;
using M13.Infrastructure.Providers;

namespace M13.Infrastructure.RepositoryServices
{
    public class RuleService : IRuleService
    {
        #region Private fields

        private readonly IRepository<Rule> _repository;
        private readonly IMapper _mapper;
        private readonly HttpClientProvider _httpClient;

        #endregion
        
        #region Ctor
        
        public RuleService(IRepository<Rule> repository, IMapper mapper, HttpClientProvider httpClient)
        {
            _repository = repository;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        
        #endregion

        public ServiceResult Add(ApplyRuleModel model)
        {
            var oldEntityId = _repository.GetAll()
                .Where(x => x.Site == model.Site)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (oldEntityId != null)
            {
                _repository.Delete(oldEntityId);
            }

            var entity = _mapper.Map<Rule>(model);
            _repository.Add(entity);

            return ServiceResult.Success;
        }

        public RuleGetModel GetRuleBySite(string site)
        {
            return _repository.GetAll()
                .Where(x => x.Site == site)
                .AsQueryable()
                .ProjectTo<RuleGetModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }

        public async Task<GetRuleTestModel> TestAsync(RuleTestModel model, CancellationToken cancellationToken)
        {
            var htmlDocument = await _httpClient.GetHtmlDocumentByPageAsync(model.Page, cancellationToken);
            var rule = model.Rule;
            var existingRule = _repository.GetAll()
                .Where(x => x.Site == model.Page)
                .Select(x => x.Principle)
                .FirstOrDefault();
            
            var innerText = string.Empty;
            var result = new GetRuleTestModel
            {
                Text = innerText
            };

            if (string.IsNullOrEmpty(rule) && string.IsNullOrEmpty(existingRule))
            {
                return result;
            }

            var temp = htmlDocument.DocumentNode
                .SelectNodes(rule ?? existingRule);
            
            /*var kek*/

            innerText = htmlDocument.DocumentNode
                .SelectNodes(rule ?? existingRule)
                .Aggregate(innerText, (current, node) => $"{current}{Environment.NewLine}{node?.InnerText}");

            result.Text = innerText;
            
            return result;
        }

        public ServiceResult DeleteRuleBySite(string site)
        {
            var entityId = _repository.GetAll()
                .Where(x => x.Site == site)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (entityId != null)
            {
                _repository.Delete(entityId);
            }
            
            return ServiceResult.Success;
        }
    }
}