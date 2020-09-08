using AutoMapper;
using M13.Domain.Entities;
using M13.Domain.Models.Rules;

namespace M13.Domain.MapperProfiles
{
    public class RuleProfile : Profile
    {
        public RuleProfile()
        {
            CreateMap<ApplyRuleModel, Rule>()
                .ForMember(x => x.Id, y => y.Ignore())
                .ForMember(x => x.Site, y => y.MapFrom(m => m.Site))
                .ForMember(x => x.Principle, y => y.MapFrom(m => m.Rule));

            CreateMap<Rule, RuleGetModel>()
                .ForMember(x => x.Rule, y => y.MapFrom(m => m.Principle));
        }
    }
}