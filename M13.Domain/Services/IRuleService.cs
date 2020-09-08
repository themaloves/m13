using System.Threading;
using System.Threading.Tasks;
using M13.Domain.Conventions;
using M13.Domain.Models.Rules;

namespace M13.Domain.Services
{
    /// <summary>
    /// Rule Service
    /// </summary>
    public interface IRuleService
    {
        ServiceResult Add(ApplyRuleModel model);
        RuleGetModel GetRuleBySite(string site);
        Task<GetRuleTestModel> TestAsync(RuleTestModel model, CancellationToken cancellationToken = default);
        ServiceResult DeleteRuleBySite(string site);
    }
}