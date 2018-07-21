using System;

namespace vega.Controllers.Resources
{
    public class UpdatePlanningAppResource
    {
        public int method { get; set; }
        public int rollbackToStateId { get; set; }
        public string CurrentStateCompletionDate { get; set; }

        public string  DateCompleted { get; set; }
    }
}