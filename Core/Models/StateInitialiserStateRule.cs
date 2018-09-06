using System.ComponentModel.DataAnnotations.Schema;
using vega.Core.Models;
using vega.Core.Models.States;

namespace vega.Core.Models
{
    [Table("StateInitialiserStateRules")]
    public class StateInitialiserStateRule
    {
        public int StateInitialiserStateId { get; set; }
        public int StateRuleId { get; set; }
        public StateInitialiserState StateInitialiserState { get; set; }
        public StateRule StateRule { get; set; }
    }
}