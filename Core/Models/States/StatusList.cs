namespace vega.Core.Models.States
{
    public static class StatusList
    {      
        public static readonly string OnTime =  "OnTime";
        public static readonly string Due =  "Due";
        public static readonly string Overdue =  "Overdue";
        public static readonly string Complete =  "Complete";
        public static readonly string Overran =  "Overran";
        public static readonly string AppInProgress =  "InProgress";
        public static readonly string AppArchived =  "Archived";
        public static readonly string AppTerminated =  "Terminated";
        public static readonly string AppError =  "Error";

        // public var planningStatusSelectorMap = new Dictionary<string, Expression<Func<PlanningApp, bool>>>()
        //     {
        //         [StatusList.AppInProgress] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppInProgress,
        //         [StatusList.AppArchived] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppArchived,
        //         [StatusList.AppTerminated] = pa => pa.CurrentPlanningStatus.Name == StatusList.AppTerminated
        //     };
    }
}