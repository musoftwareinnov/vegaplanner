namespace vega.Controllers.Resources
{
    public class PlanningAppSummaryResource
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Name { get; set; }
        public string PlanningStatus { get; set; }
        public string CurrentStateStatus { get; set; }
        public string CurrentState { get; set; }
        public string ExpectedStateCompletionDate { get; set; }
        public string NextState { get; set; }
        public string CouncilPlanningAppId{ get; set; }
        public string CompletionDate { get; set; }
    }
}