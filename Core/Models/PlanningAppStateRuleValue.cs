using System;

namespace vega.Core.Models
{
    public class PlanningAppStateRuleValue
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public string StrValue { get; set;}
        public int IntValue { get; set;}
        public DateTime DateValue { get; set;}
    }
}