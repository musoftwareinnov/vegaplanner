namespace vega.Controllers.Resources
{
    public class PlanningAppQueryResource
    {
        public int? Id { get; set; }
        public string PlanningStatus { get; set; }
        public string PlanningAppType { get; set; }   //1 = InProgress | 2 = Archived | 3 = Terminated
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}