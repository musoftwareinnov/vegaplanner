using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace vega.Controllers.Resources
{
    public class PlanningAppResource
    {
        public int Id { get; set; }
        public PlanningCustomerResource Customer { get; set; }
        public string Name { get; set; }
        public string BusinessDate { get; set; }
        public string PlanningStatus { get; set; }
        public string CurrentStateStatus { get; set; }
        public string CurrentState { get; set; }
        public string NextState { get; set; }
        public string ExpectedStateCompletionDate { get; set; }
        public string CompletionDate { get; set; }

        public ICollection<PlanningAppStateResource> PlanningAppStates { get; set; }

        public PlanningAppResource()
        {
            PlanningAppStates = new Collection<PlanningAppStateResource>();
        }
    }
}