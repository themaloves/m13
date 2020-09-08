using System.ComponentModel.DataAnnotations;

namespace M13.Domain.Models.Rules
{
    public class ApplyRuleModel
    {
        [Required]
        public string Site { get; set; }
        
        [Required]
        public string Rule { get; set; }
    }
}