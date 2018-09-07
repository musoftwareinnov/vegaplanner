namespace vega.Controllers.Resources
{
    public class StateRuleResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool isPlanningAppField { get; set; }
        public bool isMandatory { get; set; }
    }
}